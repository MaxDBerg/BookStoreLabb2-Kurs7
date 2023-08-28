using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Models;

namespace MinimalAPI_Books.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenreSeed());
            modelBuilder.ApplyConfiguration(new LanguageSeed());
            modelBuilder.ApplyConfiguration(new AuthorSeed());

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}
