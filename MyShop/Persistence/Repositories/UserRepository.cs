using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    
    public class UserRepository: BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

     
        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Orders)
                .ThenInclude(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public async Task AddAsync(User user, ERole[] userRoles)
        {
            var roles = await _context.Roles.Where(r => userRoles.Any(ur => ur.ToString() == r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole{RoleId = role.Id});
            }

            _context.Users.Add(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}