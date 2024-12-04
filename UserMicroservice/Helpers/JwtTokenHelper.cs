using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration; // Thêm namespace này

public static class JwtTokenHelper
{
    private static IConfiguration _configuration;

    // Inject IConfiguration
    public static void Configure(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string GenerateToken(IEnumerable<Claim> claims, string email, string role)
    {
        // Lấy giá trị từ file cấu hình
        var secretKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 16)
        {
            throw new ArgumentException("Invalid secret key. Ensure it is at least 16 characters long.");
        }

       

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
