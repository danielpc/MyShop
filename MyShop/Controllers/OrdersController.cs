using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Service;
using Supermarket.API.Extensions;
using Supermarket.API.Resources.Order;
using Supermarket.API.Resources.User;

namespace Supermarket.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] List<OrderItemResource> saveOrderItems)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var result = await _orderService.SaveAsync(User.GetUserID(), saveOrderItems);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _orderService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
    }
}