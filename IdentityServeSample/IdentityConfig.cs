using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServeSample
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("MyApi", "My Api")
                {
                    Scopes = new List<string>()
                    {
                        "MyApi.inner",
                        "MyApi.company"
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("BA5D32BB0CF9498CA591D38ABA95DC88".Sha256()),
                    },
                    AllowedScopes =
                    {
                        "MyApi.inner"
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope() { Name = "MyApi.inner",DisplayName = "inner use."},
                new ApiScope() { Name = "MyApi.company",DisplayName = "company use."},
            };
        }
    }
}