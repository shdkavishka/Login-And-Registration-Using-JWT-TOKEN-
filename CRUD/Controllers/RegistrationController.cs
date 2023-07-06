using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public RegistrationController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpPost]
       
        public async Task<IActionResult>Register(User model)
        {
            try
            {
                // Validate user input
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.PasswordHash))
                {
                    return BadRequest(new { Error = "Email and password are required." });
                }

                // Hash the password
                string hashedPassword = HashPassword(model.PasswordHash);

                // Save the user to the database
                User user = new User
                {
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    // Set other properties as needed
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Registration successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        private string HashPassword(string password)
        {
            // Generate a salt (work factor of 10)
            string salt = BCryptNet.GenerateSalt(10);

            // Hash the password with the salt
            string hashedPassword = BCryptNet.HashPassword(password, salt);

            return hashedPassword;
        }
    }
}
