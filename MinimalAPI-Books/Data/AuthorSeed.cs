using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Models;

namespace MinimalAPI_Books.Data
{
    public class AuthorSeed : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                new Author { Id = 1, Name = "J.K. Rowling" },
                new Author { Id = 2, Name = "Agatha Christie" },
                new Author { Id = 3, Name = "Dr. Seuss" },
                new Author { Id = 4, Name = "Stephen King" },
                new Author { Id = 5, Name = "J.R.R. Tolkien" },
                new Author { Id = 6, Name = "Jane Austen" },
                new Author { Id = 7, Name = "William Shakespeare" }
            );
        }
    }
}
