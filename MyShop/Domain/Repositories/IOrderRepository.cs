using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        void Delete(Order order);
        Task<Order> FindByIdAsync(int id);
    }
}