using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Service;
using Supermarket.API.Domain.Service.Communication;
using Supermarket.API.Resources.Order;

namespace Supermarket.API.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductService _productService;

        public ShoppingCartService(IProductService productService)
        {
            _productService = productService;
        }
        
        public  async Task<List<Item>>  GetItemsListAsync(IEnumerable<OrderItemResource> saveOrderItems)
        {
            var items = new List<Item>();

            foreach (var item in saveOrderItems)
            {
                var result = await _productService.FindByIdAsync(item.ProductId);
                items.Add(new Item {Product = result.Resource, Quantity = item.Quantity});
            }
            
            return items;
        } 
    }
}