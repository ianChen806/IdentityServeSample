using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServeSample.Client.Controllers
{
    [Route("Home/{action}")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Test()
        {
            var endpointUrl = await EndpointUrl();
            var accessToken = await AccessToken(endpointUrl);
            var response = await CallApi(accessToken);

            return response;
        }

        private async Task<string> AccessToken(string endpointUrl)
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = endpointUrl,

                ClientId = "client",
                ClientSecret = "BA5D32BB0CF9498CA591D38ABA95DC88",
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }

        private static async Task<string> CallApi(string accessToken)
        {
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);

            var response = await apiClient.GetAsync("http://localhost:5002/Home/Test");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                throw new Exception(response.StatusCode.ToString());
            }

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> EndpointUrl()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            return disco.TokenEndpoint;
        }
    }
}