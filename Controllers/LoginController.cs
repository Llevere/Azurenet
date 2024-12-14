using Azurenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Azurenet.JWT;
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


        //[HttpPost("login")]
        //public async Task<IActionResult> UserLogin(UserLogin user)
        //{
        //    var userFound = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        //    if (userFound == null) return Unauthorized("User not found!");


        //    return Ok(new {Message = userFound });
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> UserRegister(UserLogin user)
        //{
        //    User newUser = new()
        //    {
        //        Email = user.Email,
        //        PasswordHash = user.Password,
        //        CreatedAt = DateTime.UtcNow,
        //        ResetToken = null
        //    };
        //    _context.Users.Add(newUser);
        //    await _context.SaveChangesAsync();
        //    return Ok(new { Message = newUser });
        //}

        [HttpPost("get_users")]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }
            [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody]UserLogin user)
        {
            // Find the user by email
            var userFound = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userFound == null)
                return Unauthorized("User not found!");

            // Verify the password
            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(userFound, userFound.PasswordHash, user.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid password!");
            string jwt = GenerateJwtToken.GenerateToken(userFound, _configuration);
            
            return Ok(new { Message = "Login successful", User = userFound, Jwt = jwt});
        }

        
    }
}
