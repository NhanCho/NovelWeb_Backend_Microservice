

namespace Backend.Controllers
{
    using Backend.Models;
    using Backend.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);
                return Ok(new { Token = token });
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Lấy token từ header Authorization
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { Error = "Token is missing." });
                }

                var result = await _userService.LogoutAsync(token);
                return Ok(new { Message = result });
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("UpdateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest updateProfileRequest)
        {
            try
            {
                // Lấy token từ header Authorization
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { Error = "Token is missing." });
                }

                var result = await _userService.UpdateProfileAsync(
                    token: token,
                    username: updateProfileRequest.Username,
                    email: updateProfileRequest.Email,
                    password: updateProfileRequest.Password
                );

                return Ok(new { Message = result });
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }

            var success = await _userService.CreateUserAsync(user);
            if (!success)
            {
                return StatusCode(500, "Failed to create user.");
            }

            return Ok("User created successfully.");
        }



        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }

            var success = await _userService.UpdateUserAsync(id, user);
            if (!success)
            {
                return StatusCode(500, "Failed to update user.");
            }

            return Ok("User updated successfully.");
        }



        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("{userId}/followed-users")]
        public async Task<IActionResult> GetFollowedUsers(int userId)
        {
            var followedUsers = await _userService.GetFollowedUsersAsync(userId);
            return Ok(followedUsers);
        }




        [HttpPost("{userId}/follow-user/{followUserId}")]
        public async Task<IActionResult> FollowUser(int userId, int followUserId)
        {
            var success = await _userService.FollowUserAsync(userId, followUserId);
            if (!success) return BadRequest("Failed to follow user.");
            return Ok("User followed successfully.");
        }

        [HttpDelete("{userId}/unfollow-user/{followedUserId}")]
        public async Task<IActionResult> UnfollowUser(int userId, int followedUserId)
        {
            var success = await _userService.UnfollowUserAsync(userId, followedUserId);
            if (!success) return NotFound("Failed to unfollow user.");
            return NoContent();
        }

        [HttpGet("{userId}/favorite-novels")]
        public async Task<IActionResult> GetFavoriteNovels(int userId)
        {
            var favoriteNovels = await _userService.GetFavoriteNovelsAsync(userId);
            var novelIds = favoriteNovels.Select(novel => novel.NovelId).ToList();
            return Ok(novelIds);
        }


        [HttpPost("{userId}/favorite-novels/{novelId}")]
        public async Task<IActionResult> AddFavoriteNovel(int userId, int novelId)
        {
            var success = await _userService.AddFavoriteNovelAsync(userId, novelId);
            if (!success) return BadRequest("Failed to add novel to favorites.");
            return Ok("Novel added to favorites.");
        }

        [HttpDelete("{userId}/favorite-novels/{novelId}")]
        public async Task<IActionResult> RemoveFavoriteNovel(int userId, int novelId)
        {
            var success = await _userService.RemoveFavoriteNovelAsync(userId, novelId);
            if (!success) return NotFound("Failed to remove novel from favorites.");
            return NoContent();
        }


    }

}
