using BookManagement.Domain.Models;
using BookManagement.Domain.Repositories;
using BookManagement.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Book))
                return (IReadOnlyList<T>)await _dbContext.Set<Book>()
                    .Include(b => b.Category)
                    .ToListAsync();

            if (typeof(T) == typeof(Category))
                return (IReadOnlyList<T>)await _dbContext.Set<Category>()
                    .Include(c => c.Books)
                    .ToListAsync();

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Book))
            {
                var book = await _dbContext.Set<Book>()
                                           .Where(b => b.BookId == id)
                                           .Include(b => b.Category)
                                           .FirstOrDefaultAsync();
                return book as T;
            }

            if (typeof(T) == typeof(Category))
            {
                var category = await _dbContext.Set<Category>()
                                           .Where(c => c.CategoryId == id)
                                           .Include(c => c.Books)
                                           .FirstOrDefaultAsync();
                return category as T;
            }

            return await _dbContext.Set<T>().FindAsync(id);
        }


        public async Task AddAsync(T entity)
            => await _dbContext.Set<T>().AddAsync(entity);

        public void UpdateAsync(T entity)
            =>  _dbContext.Set<T>().Update(entity);

        public void DeleteAsync(T entity)
            => _dbContext.Set<T>().Remove(entity);
    }
}
