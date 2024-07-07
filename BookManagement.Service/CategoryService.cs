using BookManagement.Domain.Models;
using BookManagement.Domain.Repositories;
using BookManagement.Domain.Services;
using BookManagement.Repository;
using BookManagement.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(IGenericRepository<Category> repository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
            => await _repository.GetAllAsync();

        public async Task<Category?> GetCategoryAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddCategoryAsync(Category category)
        {
            await _repository.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _repository.UpdateAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if(category != null)
                _repository.DeleteAsync(category);

            await _dbContext.SaveChangesAsync();
        }
    }
}
