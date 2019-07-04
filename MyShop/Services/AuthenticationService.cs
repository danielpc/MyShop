using System.Threading.Tasks;
using Supermarket.API.Domain.Security.Hashing;
using Supermarket.API.Domain.Security.Tokens;
using Supermarket.API.Domain.Service;
using Supermarket.API.Domain.Service.Communication;

namespace Supermarket.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
        }
        
        public async Task<GenericResponse<AccessToken>> CreateAccessTokenAsync(string email, string password)
        {
            var user = await _userService.FindByEmailAsync(email);
            if (user == null ||!_passwordHasher.PasswordMatches(password, user.Password))
                return new GenericResponse<AccessToken>("Invalid credentials.");

            var token = _tokenHandler.CreateAccessToken(user);
            
            return new GenericResponse<AccessToken>(token);
        }

        public async Task<GenericResponse<AccessToken>> RefreshTokenAsync(string refreshToken, string userEmail)
        {
            var token = _tokenHandler.TakeRefreshToken(refreshToken);
            
            if(token == null)
                return new GenericResponse<AccessToken>("Invalid refresh token");
            
            if(token.IsExpired())
                return new GenericResponse<AccessToken>("Expired refresh token");

            var user = await _userService.FindByEmailAsync(userEmail);
            
            if(user == null)
                return new GenericResponse<AccessToken>("Invalid refresh token");

            var accessToken = _tokenHandler.CreateAccessToken(user);
            
            return new GenericResponse<AccessToken>(accessToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}