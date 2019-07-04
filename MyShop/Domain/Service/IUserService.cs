using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Service.Communication;

namespace Supermarket.API.Domain.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<GenericResponse<User>> FindByIdAsync(int id);
        Task<GenericResponse<User>> CreateUserAsync(User user, params ERole[] userRoles);
        Task<User> FindByEmailAsync(string email);
    }
}