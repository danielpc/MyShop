using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Service;
using Supermarket.API.Extensions;
using Supermarket.API.Resources.User;

namespace Supermarket.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserWithoutOrdersResource>> ListAsync()
        {
            var users = await _userService.ListAsync();
            var resource = _mapper.Map<IEnumerable<User>, IEnumerable<UserWithoutOrdersResource>>(users);
            return resource;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var result = await _userService.FindByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserWithOrdersResource>(result.Resource);
            return Ok(userResource);
        }

//        [HttpPost]
//        public async Task<IActionResult> PostAsync([FromBody] UserPost post)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState.GetErrorMessages());
//
//            var user = _mapper.Map<UserPost, User>(post);
//            var result = await _userService.SaveAsync(user);
//
//            if (!result.Success)
//                return BadRequest(result.Message);
//
//            var userResource = _mapper.Map<User, UserWithoutOrdersResource>(result.Resource);
//            return Ok(userResource);
//        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<UserCredentialsResource, User>(userCredentials);

            var result = await _userService.CreateUserAsync(user, ERole.Common);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            
            return Ok(userResource);
        }

       
       
    }
}