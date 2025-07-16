using MyAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace MyAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options){}
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<BookModel> Books { get; set; }
    }
}
