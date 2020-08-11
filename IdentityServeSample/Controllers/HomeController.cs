using System.Threading.Tasks;
using IdentityServeSample.Dto;
using IdentityServeSample.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServeSample.Controllers
{
    [ApiController]
    [Route("Home/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ITokenProvider _tokenProvider;

        public HomeController(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [HttpPost]
        public async Task<GetTokenResponse> Token([FromForm] GetTokenRequest request)
        {
            return await _tokenProvider.GetToken(request);
        }
    }
}