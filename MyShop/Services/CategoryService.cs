using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Service;
using Supermarket.API.Domain.Service.Communication;

namespace Supermarket.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRespository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRespository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<GenericResponse<Category>> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();
                return new GenericResponse<Category>(category);
            }
            catch (Exception ex)
            {
                // TODO Do some logging stuff
                return new GenericResponse<Category>($"An error occurred saving the category: {ex.Message}");
            }
        }

        public async Task<GenericResponse<Category>> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);
            
            if(existingCategory == null)
                return new GenericResponse<Category>("Category not found");

            existingCategory.Name = category.Name;

            try
            {
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();
                return new GenericResponse<Category>(existingCategory);
            }
            catch (Exception ex)
            {
                // TODO Do some logging stuff
                return new GenericResponse<Category>($"An error occurred updating the category: {ex.Message}");
            }
        }

        public async Task<GenericResponse<Category>> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);
            
            if(existingCategory == null)
                return new GenericResponse<Category>("Category not found");

            try
            {
                _categoryRepository.Delete(existingCategory);
                await _unitOfWork.CompleteAsync();
                return new GenericResponse<Category>(existingCategory);
            }
            catch (Exception ex)
            {
                // Todo Do some logging stuff
                return new GenericResponse<Category>($"An error occurred deleting the category: {ex.Message}");
            }
        }
    }
}