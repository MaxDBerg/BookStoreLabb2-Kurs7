using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Models;

namespace MinimalAPI_Books.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
