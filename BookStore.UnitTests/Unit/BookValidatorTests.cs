using BookStore.UnitTests.Factories;
using BookStore.UnitTests.Fixtures;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Unit
{
    public class BookValidatorTests : IClassFixture<BookRepositoryTestFixture>
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly BookRepositoryTestFixture _fixture;
        public BookValidatorTests(BookRepositoryTestFixture fixture)
        {
            _fixture = fixture;
            _dbContext = new BookstoreDbContext(_fixture.GetUniqueOptions());
        }

        [Theory]
        [InlineData("", "Test Description 1", 1, 1, "Title is required")]
        [InlineData("Test Title 2", "Test Description 2", -1, 2, "Language is required")]
        [InlineData("Test Title 3", "Test Description 3", 3, -2, "Author is required")]
        public async Task ShouldHaveValidationErrorFor_EmptyFieldsAsync(string title, string description, int languageId, int authorId, string errorMessage)
        {
            //arrange
            var _validator = new BookValidator(_dbContext);
            var book = BookFactory.CreateBook(title, description, languageId, authorId);

            //act
            var result = await _validator.ValidateAsync(book);

            //assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(errorMessage, result.Errors.FirstOrDefault().ErrorMessage);
        }

        [Fact]
        public async Task ShouldHaveValidationErrors_ForMultipleEmptyFieldsAsync()
        {
            // Arrange
            var _validator = new BookValidator(_dbContext);
            var book = BookFactory.CreateBook("", "", -1, -2);

            // Act
            var result = await _validator.ValidateAsync(book);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.Title) && error.ErrorMessage == "Title is required");
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.Description) && error.ErrorMessage == "Description is required");
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.LanguageId) && error.ErrorMessage == "Language is required");
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.AuthorId) && error.ErrorMessage == "Author is required");
        }

        [Fact]
        public async Task ShouldHaveValidationErrorFor_DuplicateFieldsAsync()
        {
            //arrange
            await _fixture.SeedDatabaseAsync(_dbContext);
            var _validator = new BookValidator(_dbContext);
            var book = BookFactory.CreateBook("Duplicate Title", "Test Description 1", 1, 1);
            _dbContext.Add(book);
            await _dbContext.SaveChangesAsync();

            //act
            var result = await _validator.ValidateAsync(book);

            //assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Title must be unique", result.Errors.FirstOrDefault().ErrorMessage);
        }

        [Fact]
        public void ShouldNotHaveValidationErrorFor_ValidBook()
        {
            // Arrange
            var book = BookFactory.CreateBook("Validator Name", "Interesting Description", 1, 1);
            var _validator = new BookValidator(_dbContext);


            // Act
            var result = _validator.Validate(book);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}
