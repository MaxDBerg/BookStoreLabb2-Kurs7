using AutoMapper;
using BookStore.UnitTests.Factories;
using BookStore.UnitTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.BookDTO;
using MinimalAPI_Books.Repositories;

namespace BookStore.UnitTests.Unit
{
    public class BookRepositoryTests : IClassFixture<BookRepositoryTestFixture>
    {

        private readonly BookRepositoryTestFixture _fixture;
        private readonly IMapper _mapper;

        public BookRepositoryTests(BookRepositoryTestFixture fixture)
        {
            _fixture = fixture;
            _mapper = _fixture.mapper;
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsCorrectBookAsync()
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository<BookReadDTO>(context, _mapper);

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
                var bookRepository = new BookRepository<BookReadDTO>(context, _mapper);

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
                var bookRepository = new BookRepository<BookCreateDTO>(context, _mapper);
                
                var bookDTO = BookFactory.CreateBook_DTO(bookTitle, bookDescription, bookLanguageId, bookAuthorId, _fixture.genreIds);

                //Act
                await bookRepository.AddAsync(bookDTO);
                var result = await context.Books.LastAsync();

                //Assert
                Assert.NotNull(result);
                Assert.Equal(9, result.Id);
            }
        }

        [Fact]
        public async Task UpdateAsync_WithValidBook_UpdatesBookInDatabaseAsync()
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookReadRepository = new BookRepository<BookReadDTO>(context, _mapper);
                var bookUpdateRepository = new BookRepository<BookUpdateDTO>(context, _mapper);
                var genreIdList = new List<int> { 1, 2 };
                var updatedBook = BookFactory.UpdateBookWithId_DTO(8, "Updated Title", "Updated Description", 1, 2, genreIdList);

                // Act
                await bookUpdateRepository.UpdateAsync(updatedBook);
                var book = await bookReadRepository.GetByIdAsync(8);

                // Assert
                Assert.NotNull(updatedBook);
                Assert.Equal("Updated Title", book.Title);
            }
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesBookFromDatabaseAsync()
        {
            using (var context = new BookstoreDbContext(_fixture.GetUniqueOptions()))
            {
                //Arrange
                await _fixture.SeedDatabaseAsync(context);
                var bookRepository = new BookRepository<BookReadDTO>(context, _mapper);
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
                var bookRepository = new BookRepository<BookReadDTO>(context, _mapper);
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
