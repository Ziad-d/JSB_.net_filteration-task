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
                return (IReadOnlyList<T>)await _dbContext.Set<Book>().Include(b => b.Category).ToListAsync();

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Book))
                return await _dbContext.Set<Book>().Where(b => b.BookId == id).Include(b => b.Category).ToListAsync() as T;

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
