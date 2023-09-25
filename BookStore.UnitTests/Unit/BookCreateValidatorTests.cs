using AutoMapper;
using BookStore.UnitTests.Factories;
using BookStore.UnitTests.Fixtures;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Repositories;
using MinimalAPI_Books.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Unit
{
    public class BookCreateValidatorTests : IClassFixture<BookRepositoryTestFixture>
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly BookRepositoryTestFixture _fixture;
        private readonly IMapper _mapper;

        public BookCreateValidatorTests(BookRepositoryTestFixture fixture)
        {
            _fixture = fixture;
            _mapper = _fixture.mapper;
            _dbContext = new BookstoreDbContext(_fixture.GetUniqueOptions());
        }

        [Theory]
        [InlineData("", "Test Description 1", 1, 1, "Title is required")]
        [InlineData("Test Title 2", "Test Description 2", -1, 2, "LanguageId has to be greater than 0")]
        [InlineData("Test Title 3", "Test Description 3", 3, -2, "AuthorId has to be greater than 0")]
        public async Task ShouldHaveValidationErrorFor_EmptyFieldsAsync(string title, string description, int languageId, int authorId, string errorMessage)
        {
            //arrange
            var _validator = new BookCreateDTOValidator(_dbContext);
            var book = BookFactory.CreateBook_DTO(title, description, languageId, authorId, _fixture.genreIds);

            //act
            var result = await _validator.ValidateAsync(book);

            //assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(errorMessage, result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ShouldHaveValidationErrors_ForMultipleEmptyFieldsAsync()
        {
            // Arrange
            var _validator = new BookCreateDTOValidator(_dbContext);
            var book = BookFactory.CreateBook_DTO("", "", -1, -2, _fixture.genreIds);

            // Act
            var result = await _validator.ValidateAsync(book);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.Title) && error.ErrorMessage == "Title is required");
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.Description) && error.ErrorMessage == "Description is required");
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.LanguageId) && error.ErrorMessage == "LanguageId has to be greater than 0");
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(Book.AuthorId) && error.ErrorMessage == "AuthorId has to be greater than 0");
        }

        [Fact]
        public async Task ShouldHaveValidationErrorFor_DuplicateFieldsAsync()
        {
            //arrange
            await _fixture.SeedDatabaseAsync(_dbContext);
            var _validator = new BookCreateDTOValidator(_dbContext);
            var book = BookFactory.CreateBook_DTO("Duplicate Title", "Test Description 1", 1, 1, _fixture.genreIds);
            var repository = new BookRepository(_dbContext, _mapper);
            await repository.AddAsync(book);

            //act
            var result = await _validator.ValidateAsync(book);

            //assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Title must be unique", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldNotHaveValidationErrorFor_ValidBook()
        {
            // Arrange
            var book = BookFactory.CreateBook_DTO("Validator Name", "Interesting Description", 1, 1, _fixture.genreIds);
            var _validator = new BookCreateDTOValidator(_dbContext);


            // Act
            var result = _validator.Validate(book);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}
