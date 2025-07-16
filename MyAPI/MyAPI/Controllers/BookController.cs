using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Services;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService) { 
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<List<BookModelDTO>> GetAllBooks()
        {
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread ID: {threadId}");
            return await _bookService.GetBooks();
        }

        [HttpGet("{title}")]
        public async Task<List<BookModelDTO>> GetBookTitle(string title) {
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread ID: {threadId}");
            return await _bookService.GetBookByTitle(title);
        }

        [HttpPost]
        public void AddBook([FromBody] BookModelDTO book) {
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread ID: {threadId}");
            BookModel bookModel = new BookModel
            {
                Author = new AuthorModel { Name = book.Author.Name },
                Title = book.Title
            };
            _bookService.AddBook(bookModel);
        }
    }
}
