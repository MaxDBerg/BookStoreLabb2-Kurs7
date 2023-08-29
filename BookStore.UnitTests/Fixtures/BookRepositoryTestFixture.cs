using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Fixtures
{
    public class BookRepositoryTestFixture : IDisposable
    {
        public DbContextOptions<BookstoreDbContext> _options { get; private set; }

        public BookRepositoryTestFixture()
        {
            _options = new DbContextOptionsBuilder<BookstoreDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }
        public DbContextOptions<BookstoreDbContext> GetUniqueOptions()
        {
            var databaseName = Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<BookstoreDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
        }
        public async Task SeedDatabaseAsync(BookstoreDbContext context)
        {
            //Seed the database with test data
            var authors = new List<Author> {
                new Author { Id = 1, Name = "testAuthorName1"},
                new Author { Id = 2, Name = "testAuthorName2"},
                new Author { Id = 3, Name = "testAuthorName3"},
                new Author { Id = 4, Name = "testAuthorName4"}
            };

            var languages = new List<Language> {
                new Language { Id = 1, Name = "testLanguageName1"},
                new Language { Id = 2, Name = "testLanguageName2"},
                new Language { Id = 3, Name = "testLanguageName3"},
                new Language { Id = 4, Name = "testLanguageName4"}
            };

            var genres = new List<Genre> {
                new Genre { Id = 1, Name = "testGenreName1"},
                new Genre { Id = 2, Name = "testGenreName2"},
                new Genre { Id = 3, Name = "testGenreName3"},
                new Genre { Id = 4, Name = "testGenreName4"}
            };

            var books = new List<Book> {
                new Book { Id = 1, Title = "Test1", LanguageId = 1, AuthorId = 1, Description = "TestDescription1", IsLoanable = true, Publication = DateTime.Now },
                new Book { Id = 2, Title = "Test2", LanguageId = 1, AuthorId = 1, Description = "TestDescription2", IsLoanable = true, Publication = DateTime.Now },
                new Book { Id = 3, Title = "Test3", LanguageId = 1, AuthorId = 1, Description = "TestDescription3", IsLoanable = true, Publication = DateTime.Now },
                new Book { Id = 4, Title = "Test4", LanguageId = 1, AuthorId = 1, Description = "TestDescription4", IsLoanable = true, Publication = DateTime.Now },
                new Book { Id = 5, Title = "Test5", LanguageId = 1, AuthorId = 1, Description = "TestDescription5", IsLoanable = true, Publication = DateTime.Now },
                new Book { Id = 6, Title = "Test6", LanguageId = 1, AuthorId = 1, Description = "TestDescription6", IsLoanable = true, Publication = DateTime.Now },
                new Book { Id = 7, Title = "Test7", LanguageId = 1, AuthorId = 1, Description = "TestDescription7", IsLoanable = true, Publication = DateTime.Now },
                new Book { Id = 8, Title = "Test8", LanguageId = 1, AuthorId = 1, Description = "TestDescription8", IsLoanable = true, Publication = DateTime.Now }
            };

            //Adding the data to in-memory database
            await context.Authors.AddRangeAsync(authors);
            await context.Languages.AddRangeAsync(languages);
            await context.Genres.AddRangeAsync(genres);
            await context.Books.AddRangeAsync(books);

            //_testDbContext.Authors.AddRange(authors);
            //_testDbContext.Languages.AddRange(languages);
            //_testDbContext.Genres.AddRange(genres);
            //_testDbContext.Books.AddRange(books);

            context.SaveChanges();
        }
        public void Dispose()
        {
        }
    }
}
