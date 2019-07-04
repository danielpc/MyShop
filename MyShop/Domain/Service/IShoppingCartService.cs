using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Service.Communication;
using Supermarket.API.Resources.Order;

namespace Supermarket.API.Domain.Service
{
    public interface IShoppingCartService
    {
        Task<List<Item>> GetItemsListAsync(IEnumerable<OrderItemResource> saveOrderItems);
    }
}