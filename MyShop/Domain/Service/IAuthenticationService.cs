using System.Threading.Tasks;
using Supermarket.API.Domain.Security.Tokens;
using Supermarket.API.Domain.Service.Communication;

namespace Supermarket.API.Domain.Service
{
    public interface IAuthenticationService
    {
        Task<GenericResponse<AccessToken>> CreateAccessTokenAsync(string email, string password);
        Task<GenericResponse<AccessToken>> RefreshTokenAsync(string refreshToken, string userEmail);
        void RevokeRefreshToken(string refreshToken);
    }
}