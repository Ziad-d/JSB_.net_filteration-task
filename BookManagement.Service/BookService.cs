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
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly ApplicationDbContext _dbContext;

        public BookService(IGenericRepository<Book> repository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<Book>> GetBooksAsync()
            => await _repository.GetAllAsync();

        public async Task<Book?> GetBookAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddBookAsync(Book book)
        {
            await _repository.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _repository.UpdateAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if(book != null)
            {
                _repository.DeleteAsync(book);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
