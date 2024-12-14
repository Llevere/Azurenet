using Microsoft.AspNetCore.Mvc;

namespace Azurenet.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class JWTController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;


    }
}
