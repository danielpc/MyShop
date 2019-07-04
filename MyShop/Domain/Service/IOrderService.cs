using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Service.Communication;
using Supermarket.API.Resources.Order;

namespace Supermarket.API.Domain.Service
{
    public interface IOrderService
    {
        Task<GenericResponse<Order>> SaveAsync(int userId,  List<Item> items);
        Task<GenericResponse< List<Item>>> GetItemsListAsync(IEnumerable<OrderItemResource> saveOrderItems);
        Task<GenericResponse<Order>> DeleteAsync(int id);
    }
}