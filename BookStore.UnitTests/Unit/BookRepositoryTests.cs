using BookStore.UnitTests.Factories;
using BookStore.UnitTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;

namespace BookStore.UnitTests.Unit
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
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository(context);

                //Act
                var result = await bookRepository.GetByIdAsync(2);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
            }
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfBooksAsync()
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository(context);

                //Act
                var result = await bookRepository.GetAllAsync();

                //Assert
                Assert.NotNull(result);
                Assert.Equal(8, result.Count());
            }
        }

        [Theory]
        [InlineData("Test Title", "Test Description 1", 1, 1)]
        [InlineData("Test Title 2", "Test Description 2", 2, 2)]
        [InlineData("Test Title 3", "Test Description 3", 3, 3)]
        public async Task AddAsync_WithValidData_AddsBookToDatabaseAsync(string bookTitle, string bookDescription, int bookLanguageId, int bookAuthorId)
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository(context);
                
                var book = BookFactory.CreateBook(bookTitle, bookDescription, bookLanguageId, bookAuthorId);

                //Act
                await bookRepository.AddAsync(book, 2);
                var result = await context.Books.FindAsync(book.Id);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(book, result);
            }
        }

        [Fact]
        public async Task UpdateAsync_WithValidBook_UpdatesBookInDatabaseAsync()
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository(context);
                var updatedTitle = "Updated Title";
                var book = await bookRepository.GetByIdAsync(1);

                // Act
                book.Title = updatedTitle;
                await bookRepository.UpdateAsync(book);

                // Assert
                var updatedBook = await context.Books.FindAsync(book.Id);
                Assert.NotNull(updatedBook);
                Assert.Equal(updatedTitle, updatedBook.Title);
            }
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesBookFromDatabaseAsync()
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository(context);
                var book = await bookRepository.GetByIdAsync(1);

                //Act
                await bookRepository.DeleteAsync(book.Id);

                //Assert
                var deletedBook = await context.Books.FindAsync(book.Id);
                Assert.Null(deletedBook);
            }
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_DoesNotDeleteBookAsync()
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository(context);
                var book = await bookRepository.GetByIdAsync(1);

                //Act
                await bookRepository.DeleteAsync(100);

                //Assert
                var deletedBook = await context.Books.FindAsync(book.Id);
                Assert.NotNull(deletedBook);
            }

        }
    }

}
