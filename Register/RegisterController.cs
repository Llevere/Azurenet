using Azurenet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azurenet.Register
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RegisterController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;


        [HttpPost("register")]
        public async Task<IActionResult> UserRegister([FromBody] UserLogin user)
        {
            // Hash the password before storing it
            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null!, user.Password);

            User newUser = new()
            {
                Email = user.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                ResetToken = null
            };

            // Save the user
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully", User = newUser });
        }
    }
}
