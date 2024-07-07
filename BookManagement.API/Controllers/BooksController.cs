using BookManagement.API.DTOs;
using BookManagement.Domain.Models;
using BookManagement.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Book>>> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] BookDTO bookDTO)
        {
            var book = new Book
            {
                Name = bookDTO.Name,
                Description = bookDTO.Description,
                Price = bookDTO.Price,
                Author = bookDTO.Author,
                Stock = bookDTO.Stock,
                CategoryId = bookDTO.CategoryId,
            };

            await _bookService.AddBookAsync(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook([FromRoute] int id, [FromBody] BookDTO bookDTO)
        {
            var existingBook = await _bookService.GetBookAsync(id);

            if (existingBook == null)
                return NotFound();

            if(id != existingBook.BookId)
                return BadRequest();

            existingBook.Name = bookDTO.Name;
            existingBook.Description = bookDTO.Description;
            existingBook.Price = bookDTO.Price;
            existingBook.Author = bookDTO.Author;
            existingBook.Stock = bookDTO.Stock;
            existingBook.CategoryId = bookDTO.CategoryId;

            await _bookService.UpdateBookAsync(existingBook);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}
