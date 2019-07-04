using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Service;
using Supermarket.API.Domain.Service.Communication;
using Supermarket.API.Resources.Order;

namespace Supermarket.API.Services
{
    
    public class OrderService : IOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;


        public OrderService(IUserRepository userRepository, IProductService productService, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _productService = productService;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<GenericResponse<Order>> SaveAsync(int userId, List<Item> items)
        {
            var existingUser = await _userRepository.FindByIdAsync(userId);
            
            if (existingUser == null)
                return new GenericResponse<Order>("No user found");

            try
            {
                var order = new Order {Created = DateTime.Now, Items = items};
                existingUser.Orders.Add(order);
                
                _userRepository.Update(existingUser);
                await _orderRepository.AddAsync(order);
                await _unitOfWork.CompleteAsync();
                return new GenericResponse<Order>(order);
            }
            catch (Exception ex)
            {
                return new GenericResponse<Order>($"An error occurred saving the order: {ex.Message}");
            }
            
        }

        public async Task<GenericResponse< List<Item>>> GetItemsListAsync(IEnumerable<OrderItemResource> saveOrderItems)
        {
            var items = new List<Item>();
            try
            {
                foreach (var item in saveOrderItems)
                {
                    var result = await _productService.FindByIdAsync(item.ProductId);

                    if (!result.Success)
                        return new GenericResponse< List<Item>>(result.Message);
                
                    items.Add(new Item{ Product = result.Resource, Quantity = item.Quantity});
                }
                return new GenericResponse< List<Item>>(items);
            }
            catch (Exception ex)
            {
                return new GenericResponse< List<Item>>($"Could not get the items: {ex.Message}");
            }
            
        }

        public async Task<GenericResponse<Order>> DeleteAsync(int id)
        {
            var existingOrder = await _orderRepository.FindByIdAsync(id);
            
            if(existingOrder == null)
                return new GenericResponse<Order>("Order not found");

            try
            {
                _orderRepository.Delete(existingOrder);
                await _unitOfWork.CompleteAsync();
                return new GenericResponse<Order>(existingOrder);
            }
            catch (Exception ex)
            {
                return new GenericResponse<Order>($"An error occured deleting the order: {ex.Message}");
            }
        }
    }
}