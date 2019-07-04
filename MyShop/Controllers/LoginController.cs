using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Security.Tokens;
using Supermarket.API.Domain.Service;
using Supermarket.API.Resources.Token;
using Supermarket.API.Resources.User;

namespace Supermarket.API.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public LoginController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }
        
        
        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginResource userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.CreateAccessTokenAsync(userLogin.Email, userLogin.Password);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            var accessTokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Resource);
            return Ok(accessTokenResource);
        } 
        
        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.UserEmail);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }
           
            var tokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Resource);
            return Ok(tokenResource);
        }

        [Route("/api/token/revoke")]
        [HttpPost]
        public IActionResult RevokeToken([FromBody] RevokeTokenResource revokeTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authenticationService.RevokeRefreshToken(revokeTokenResource.Token);
            return NoContent();
        }
    }
}