using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserMicroservice.Models;
using UserMicroservice.Models.DTOs;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFollowedUserService _followedUserService;
        private readonly IFavoriteNovelService _favoriteNovelService;

        public UserController(
            IUserService userService,
            IFollowedUserService followedUserService,
            IFavoriteNovelService favoriteNovelService)
        {
            _userService = userService;
            _followedUserService = followedUserService;
            _favoriteNovelService = favoriteNovelService;
        }

        // ============================
        // CRUD for Users
        // ============================

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetUserById/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                return NotFound("User not found.");
            return Ok(user);
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            _userService.AddUser(user);
            return Ok("User added successfully.");
        }

        [HttpPut("UpdateUser/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] User user)
        {
            var existingUser = _userService.GetUserById(userId);
            if (existingUser == null)
                return NotFound("User not found.");

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;

            _userService.UpdateUser(existingUser);
            return Ok("User updated successfully.");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                return NotFound("User not found.");

            _userService.DeleteUser(userId);
            return Ok("User deleted successfully.");
        }

        // ============================
        // Followed Users
        // ============================

        [HttpGet("GetFollowedUsers/{userId}")]
        public IActionResult GetFollowedUsers(int userId)
        {
            var followedUsers = _followedUserService.GetFollowedUsersByUserId(userId);
            return Ok(followedUsers);
        }

        [HttpPost("FollowUser")]
        public IActionResult FollowUser([FromBody] FollowedUser followedUser)
        {
            _followedUserService.AddFollowedUser(followedUser);
            return Ok("User followed successfully.");
        }

        [HttpDelete("UnfollowUser/{userId}/{followedUserId}")]
        public IActionResult UnfollowUser(int userId, int followedUserId)
        {
            _followedUserService.RemoveFollowedUser(userId, followedUserId);
            return Ok("User unfollowed successfully.");
        }

        // ============================
        // Favorite Novels
        // ============================

        [HttpGet("GetFavoriteNovels/{userId}")]
        public IActionResult GetFavoriteNovels(int userId)
        {
            var favoriteNovels = _favoriteNovelService.GetFavoriteNovelsByUserId(userId);
            return Ok(favoriteNovels);
        }

        [HttpPost("AddFavoriteNovel")]
        public IActionResult AddFavoriteNovel([FromBody] FavoriteNovel favoriteNovel)
        {
            _favoriteNovelService.AddFavoriteNovel(favoriteNovel);
            return Ok("Novel added to favorites successfully.");
        }

        [HttpDelete("RemoveFavoriteNovel/{userId}/{novelId}")]
        public IActionResult RemoveFavoriteNovel(int userId, int novelId)
        {
            _favoriteNovelService.RemoveFavoriteNovel(userId, novelId);
            return Ok("Novel removed from favorites successfully.");
        }

        // ============================
        // Authentication
        // ============================

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Tìm user trong database
            var user = _userService.GetAllUsers()
                                   .FirstOrDefault(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            // Tạo danh sách claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // Thêm UserID vào token
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            // Sinh JWT Token
            var token = JwtTokenHelper.GenerateToken(
                claims: claims,
                email: user.Email,
                role: user.Role
                );

            return Ok(new { Token = token });
        }


        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Lấy thông tin user ID từ token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User is not logged in.");
            }

            var userId = int.Parse(userIdClaim.Value);

            // Lấy token từ header Authorization
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is missing.");
            }


            // Log hoặc xử lý thêm nếu cần
            return Ok(new { Message = "User logged out successfully.", UserID = userId  });
        }




        // ============================
        // Profile
        // ============================

        [HttpPut("UpdateProfile")]
        [Authorize] // Chỉ người dùng có token hợp lệ mới được phép
        public IActionResult UpdateProfile([FromBody] UpdateProfileRequest updateProfileRequest)
        {
            // Lấy thông tin user ID từ token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User is not logged in.");
            }

            var userId = int.Parse(userIdClaim.Value);

            // Kiểm tra dữ liệu đầu vào hợp lệ
            if (string.IsNullOrWhiteSpace(updateProfileRequest.Username) ||
                string.IsNullOrWhiteSpace(updateProfileRequest.Email) ||
                string.IsNullOrWhiteSpace(updateProfileRequest.Password))
            {
                return BadRequest("Username, Email, and Password cannot be empty.");
            }

            if (!updateProfileRequest.Email.Contains("@"))
            {
                return BadRequest("Invalid email format.");
            }

            // Kiểm tra xem email đã tồn tại hay chưa (tránh trùng lặp)
            var existingUser = _userService.GetAllUsers()
                                           .FirstOrDefault(u => u.Email == updateProfileRequest.Email && u.UserID != userId);
            if (existingUser != null)
            {
                return Conflict("Email is already in use by another user.");
            }

            // Lấy user từ database
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Cập nhật thông tin
            user.Username = updateProfileRequest.Username;
            user.Email = updateProfileRequest.Email;
            user.Password = updateProfileRequest.Password;

            _userService.UpdateUser(user);

            return Ok(new
            {
                Message = "Profile updated successfully.",
                UpdatedUser = new
                {
                    user.UserID,
                    user.Username,
                    user.Email
                }
            });
        }




    }
}
