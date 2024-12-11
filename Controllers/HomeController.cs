using Azurenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Azurenet.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class HomeController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        //Dapper
        [HttpGet("dapper")]
        public async Task<IActionResult> GetDapper()
        {
            var tests = await _context.Tests
               .FromSqlRaw("SELECT * FROM User")
               .ToListAsync();

            return Ok(tests);
        }

        [HttpGet("efcore")]
        public async Task<IActionResult> GetEFCore()
        {
            var Users = await _context.Users.ToListAsync();
            return Ok(Users);
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
