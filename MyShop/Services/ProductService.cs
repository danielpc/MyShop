using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Service;
using Supermarket.API.Domain.Service.Communication;

namespace Supermarket.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<GenericResponse<Product>> FindByIdAsync(int id)
        {
            var product =  await _productRepository.FindByIdAsync(id);
            if (product == null) 
                return new GenericResponse<Product>("No product found");
            
            return new GenericResponse<Product>(product);
        }
    }
}