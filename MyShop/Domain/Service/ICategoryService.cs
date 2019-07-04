using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Service.Communication;

namespace Supermarket.API.Domain.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<GenericResponse<Category>> SaveAsync(Category category);
        Task<GenericResponse<Category>> UpdateAsync(int id, Category category);
        Task<GenericResponse<Category>> DeleteAsync(int id);
    
    }
}