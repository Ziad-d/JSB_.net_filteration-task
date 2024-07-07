using BookManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Domain.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
