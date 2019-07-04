using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindByIdAsync(int id);
        void Update(User user);
        Task AddAsync(User user, ERole[] userRoles);
        Task<User> FindByEmailAsync(string email);
    }
}