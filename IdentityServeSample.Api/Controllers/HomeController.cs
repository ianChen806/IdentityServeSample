using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServeSample.Api.Controllers
{
    [Route("Home/{action}")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public JsonResult Test()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public JsonResult TestAdmin()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public JsonResult TestUser()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}