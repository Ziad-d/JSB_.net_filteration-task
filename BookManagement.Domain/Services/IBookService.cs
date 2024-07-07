using BookManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Domain.Services
{
    public interface IBookService
    {
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task<Book?> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
