using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using System;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;
namespace MyAPI.Services
{
    public class BookServie : IBookService
    {
        private readonly MyDbContext _context;

        public BookServie(MyDbContext context)
        {
            _context=context;
        }
        public async Task<List<BookModelDTO>> GetBooks()
        {
            List<BookModelDTO> books = new List<BookModelDTO>();
            foreach (BookModel b in await _context.Books.Include(b => b.Author).ToListAsync()){
                BookModelDTO temp = new BookModelDTO { Title= b.Title, Author=new AuthorModelDTO {Name=b.Author.Name} };
                books.Add(temp);
            }
            return books;
        }
        public async Task<List<BookModelDTO>> GetBookByTitle(string title)
        {
            List<BookModelDTO> books = new List<BookModelDTO>();
            foreach (BookModel b in await _context.Books.Include(b => b.Author).Where(b => b.Title.ToLower().Contains(title.ToLower())).ToListAsync())
            {
                BookModelDTO temp = new BookModelDTO { Title = b.Title, Author = new AuthorModelDTO { Name = b.Author.Name } };
                books.Add(temp);
            }
            return books;
        }
        public async Task AddBook(BookModel book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }
    }
 
}
