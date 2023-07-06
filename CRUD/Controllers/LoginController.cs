using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public LoginController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        
        public async Task<IActionResult> Login(User model)
        {
            try
            {
                // Retrieve the user from the database based on the email
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (user == null)
                {
                    // User not found
                    return Unauthorized(new { Error = "Invalid email or password." });
                }

                // Compare the provided password with the stored hashed password
                if (!VerifyPasswordHash(model.PasswordHash, user.PasswordHash))
                {
                    // Password does not match
                    return Unauthorized(new { Error = "Invalid email or password." });
                }

                // Generate JWT token
                string token = GenerateJwtToken(user);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        private bool VerifyPasswordHash(string password, string hashedPassword)
        {
            // Implement password verification logic
            // Example: use a secure hashing algorithm like BCrypt to compare the hashed password with the provided password
            // Return true if the passwords match; otherwise, return false
            return BCryptNet.Verify(password, hashedPassword);
        }

        private string GenerateJwtToken(User user)
        {
            // Retrieve the secret key from configuration
            string secretKey = "abcdefghijklmnopqrst";// Replace with your own secret key

            // Convert the secret key to bytes
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            // Ensure the key size is sufficient
            if (keyBytes.Length < 16)
            {
                throw new ApplicationException("Invalid key size. The key size must be at least 128 bits (16 bytes).");
            }

            // Truncate or pad the key to 128 bits (16 bytes)
            byte[] key = new byte[128];
            Array.Copy(keyBytes, key, Math.Min(keyBytes.Length, 16));

            // Create the token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            // Add claims as needed (e.g., user id, roles)
            new Claim("userId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1), // Set the token expiration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Convert the token to a string
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }

}
