using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServeSample.Dto
{
    public class GetTokenRequest
    {
        [FromForm(Name = "username")]
        public string Username { get; set; }

        [FromForm(Name = "password")]
        public string Password { get; set; }

        [JsonProperty("grant_type")]
        [FromForm(Name = "grant_type")]
        public string GrantType { get; set; }

        [FromForm(Name = "scope")]
        public string Scope { get; set; }

        [FromForm(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}