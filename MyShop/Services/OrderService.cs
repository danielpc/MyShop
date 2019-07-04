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
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;


        public OrderService(IUserRepository userRepository, IShoppingCartService shoppingCartService, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _shoppingCartService = shoppingCartService;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<GenericResponse<Order>> SaveAsync(int userId, IEnumerable<OrderItemResource> saveOrderItems)
        {
            var existingUser = await _userRepository.FindByIdAsync(userId);
            var items = await _shoppingCartService.GetItemsListAsync(saveOrderItems);
            
            if(items.Count == 0)
                return new GenericResponse<Order>("No items made");
            
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