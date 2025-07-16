using MyAPI.Services;
using MyAPI.Models;
namespace MyAPI.Services
{
    public interface IBookService
    {
        Task<List<BookModelDTO>> GetBooks();
        Task<List<BookModelDTO>> GetBookByTitle(string title);
        Task AddBook(BookModel name);
    }
}
