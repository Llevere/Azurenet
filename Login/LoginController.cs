using Azurenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Azurenet.JWT;
using Azurenet.Login;
namespace Azurenet.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;
        private IConfiguration _configuration;

        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
            _configuration = configuration;
        }


        [HttpGet("get_users")]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginModel user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return BadRequest("Invalid login data!");


            // Find the user by email
            var userFound = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userFound == null)
                return Unauthorized("User not found!");

            // Compare passwords
            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(userFound, userFound.PasswordHash, user.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid password!");

            //Genereate JWT
            string jwt = GenerateJwtToken.GenerateToken(userFound, _configuration);

            //Send back neccessary user data
            var userInfo = new
            {
                userFound.Email,
                userFound.Role,
            };

            return Ok(new { Message = "Login successful", User = userInfo, Jwt = jwt });
        }


    }
}
