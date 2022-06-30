using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBackend.Data;
using WebBackend.Data.Entities;

namespace WebBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly AppEFContext _context;
        public UsersController(AppEFContext appEFContext) => _context = appEFContext;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<UserEntity> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
