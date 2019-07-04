using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Service.Communication;


namespace Supermarket.API.Domain.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<GenericResponse<Product>> FindByIdAsync(int id);
    }
}