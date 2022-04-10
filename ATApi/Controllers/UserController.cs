using ATApi.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_context.Users.ToList());
        }
    }
}
