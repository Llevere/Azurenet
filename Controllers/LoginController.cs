using Azurenet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Azurenet.Cryptography;
namespace Azurenet.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class LoginController(AppDbContext context, IConfiguration configuration) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(UserLogin user)
        {
            var userFound = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userFound == null) return Unauthorized("User not found!");


            return Ok(new {Message = userFound });
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(UserLogin user)
        {
            User newUser = new()
            {
                Email = user.Email,
                PasswordHash = user.Password,
                CreatedAt = DateTime.UtcNow,
                ResetToken = null
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(new { Message = newUser });
        }
    }
}
