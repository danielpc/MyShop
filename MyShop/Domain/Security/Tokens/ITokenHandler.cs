using Microsoft.AspNetCore.Authentication.Twitter;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Security.Tokens
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}