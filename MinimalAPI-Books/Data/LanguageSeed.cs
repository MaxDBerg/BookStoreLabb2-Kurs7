using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Models;

namespace MinimalAPI_Books.Data
{
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
