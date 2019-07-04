using System.Linq;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
            
        }
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<Order> FindByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }
    }
}