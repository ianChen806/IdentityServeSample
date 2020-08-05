using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServeSample.Api.Controllers
{
    [Authorize]
    [Route("Home/{action}")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public JsonResult Test()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}