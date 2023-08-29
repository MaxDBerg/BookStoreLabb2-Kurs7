using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;

namespace BookStore.UnitTests
{
    public class BookRepositoryTests : IClassFixture<BookRepositoryTestFixture>
    {
        
        private readonly BookRepositoryTestFixture _fixture;
        public BookRepositoryTests(BookRepositoryTestFixture fixture)
        {

            _fixture = fixture;
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsCorrectBookAsync()
        {
           //Arrange
           await _fixture.SeedDatabase();

           //Act
           var result = await _fixture._bookRepository.GetByIdAsync(2);

           //Assert
           Assert.NotNull(result);
           Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfBooksAsync()
        {
           //Arrange
           await _fixture.SeedDatabase();

           //Act
           var result = await _fixture._bookRepository.GetAllAsync();

           //Assert
           Assert.NotNull(result);
           Assert.Equal(7, result.Count());
        }

        [Fact]
        public async Task AddAsync_WithValidData_AddsBookToDatabaseAsync()
        {
            //Arrange
            await _fixture.SeedDatabase();
            var book = new Book
            {
                Title = "Test Book",
                Description = "Test Description",
                LanguageId = 1,
                AuthorId = 1,
                Publication = DateTime.Now,
                IsLoanable = true,
            };

            //Act
            await _fixture._bookRepository.AddAsync(book, 2);
            var result = await _fixture._testDbContext.Books.FindAsync(book.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(book, result);
        }

        [Fact]
        public async Task UpdateAsync_WithValidBook_UpdatesBookInDatabaseAsync()
        {
            //Arrange
            await _fixture.SeedDatabase();
            var updatedTitle = "Updated Title";
            var book = await _fixture._bookRepository.GetByIdAsync(1);

            // Act
            book.Title = updatedTitle;
            await _fixture._bookRepository.UpdateAsync(book);

            // Assert
            var updatedBook = await _fixture._testDbContext.Books.FindAsync(book.Id);
            Assert.NotNull(updatedBook);
            Assert.Equal(updatedTitle, updatedBook.Title);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesBookFromDatabase()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_DoesNotDeleteBook()
        {
            //Arrange
            //Act
            //Assert
        }
    }

}
