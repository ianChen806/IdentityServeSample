using System.Threading.Tasks;
using IdentityServeSample.Dto;

namespace IdentityServeSample.Interface
{
    public interface ITokenProvider
    {
        Task<GetTokenResponse> GetToken(GetTokenRequest request);
    }
}