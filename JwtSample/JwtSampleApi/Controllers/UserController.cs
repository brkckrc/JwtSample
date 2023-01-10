using JwtSampleApi.Models;
using JwtSampleApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IJWTManagerRepository jwtManagerRepository;

        public UserController(IJWTManagerRepository jwtManagerRepository)
        {
            this.jwtManagerRepository = jwtManagerRepository;
        }

        [HttpGet]
        [Route("userlist")]
       public List<string> Get()
        {
            var users = new List<string>
            {
                "Test User 1",
                "Test User 2",
                "Test User 3"
            };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users userData )
        {
            var token = jwtManagerRepository.Authenticate(userData);

            if (token == null)
            {
                return Unauthorized("Unauthorized!");
            }
            else return Ok(token);
        }
    }
}
