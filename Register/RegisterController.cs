using Azurenet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Azurenet.Register
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RegisterController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;


        [HttpPost("register")]
        public async Task<IActionResult> UserRegister([FromBody] RegisterModel user)
        {
            // Check if the role exists in the UserRoles enum
            if (!Enum.TryParse<UserRoles>(user.Role, ignoreCase: true, out var parsedRole))
            {
                // If the role is not found in the enum, return an error
                return BadRequest(new { Message = "Invalid role specified." });
            }

            //Find if email already exists
            var emailExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (emailExists == null)
            {
                return BadRequest(new { Message = $"Email {user.Email} already exists" });
            }

            // Hash the password before storing it
            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null!, user.Password);

            User newUser = new()
            {
                Email = user.Email,
                PasswordHash = hashedPassword,
                Role = parsedRole.ToString(),
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
