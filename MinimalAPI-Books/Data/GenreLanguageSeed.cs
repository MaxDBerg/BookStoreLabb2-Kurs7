namespace MinimalAPI_Books.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MinimalAPI_Books.Models;

    public class GenreLanguageSeed : IEntityTypeConfiguration<Genre>
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

    public class LanguageSeed : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasData(
                new Language { Id = 1, Name = "English" },
                new Language { Id = 2, Name = "Spanish" },
                new Language { Id = 3, Name = "French" },
                new Language { Id = 4, Name = "German" },
                new Language { Id = 5, Name = "Italian" },
                new Language { Id = 6, Name = "Portuguese" },
                new Language { Id = 7, Name = "Russian" },
                new Language { Id = 8, Name = "Swedish" },
                new Language { Id = 9, Name = "Japanese" }
            ); ;
        }
    }

}
