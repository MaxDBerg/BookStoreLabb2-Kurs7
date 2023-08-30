namespace MinimalAPI_Books.Seeding
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MinimalAPI_Books.Models;

    public class GenreSeed : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre { Id = 1, Name = "Mystery" },
                new Genre { Id = 2, Name = "Fantasy" },
                new Genre { Id = 3, Name = "Romance" },
                new Genre { Id = 4, Name = "Thriller" },
                new Genre { Id = 5, Name = "Science Fiction" },
                new Genre { Id = 6, Name = "Western" },
                new Genre { Id = 7, Name = "Dystopian" },
                new Genre { Id = 8, Name = "Contemporary" },
                new Genre { Id = 9, Name = "Memoir" },
                new Genre { Id = 10, Name = "Cooking" },
                new Genre { Id = 11, Name = "Art" },
                new Genre { Id = 12, Name = "Self-help / Personal" },
                new Genre { Id = 13, Name = "Health" },
                new Genre { Id = 14, Name = "History" },
                new Genre { Id = 15, Name = "Travel" }
            ); ;
        }
    }
}
