using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Supermarket.API.Controllers
{
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("/api/protectedforcommonusers")]
        public IActionResult GetProtectedData()
        {
            
             return Ok($"Hello world from protected controller, you have the subject: {User.Identity.Name}");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("/api/protectedforadministrators")]
        public IActionResult GetProtectedDataForAdmin()
        {
            return Ok("Hello admin!.");
        }
    }
}