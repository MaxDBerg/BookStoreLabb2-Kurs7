using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.UnitTests.Factories;
using BookStore.UnitTests.Fixtures;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Validators;

namespace BookStore.UnitTests.Unit
{
    public class BookUpdateValidatorTests : IClassFixture<BookRepositoryTestFixture>
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly BookRepositoryTestFixture _fixture;

        public BookUpdateValidatorTests(BookRepositoryTestFixture fixture)
        {
            _fixture = fixture;
            _dbContext = new BookstoreDbContext(_fixture.GetUniqueOptions());
        }

        [Fact]
        public async Task ShouldHaveValidationErrorFor_NoMatchingPrimaryKeyAsync()
        {
            //arrange
            await _fixture.SeedDatabaseAsync(_dbContext);
            var _validator = new BookUpdateDTOValidator(_dbContext);
            var genreIdList = new List<int> { 1, 2 };
            var book = BookFactory.UpdateBookWithId_DTO(9, "test123", "test123desc", 1, 2, genreIdList);

            //act
            var result = await _validator.ValidateAsync(book);

            //assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Book with Id: 9 does not exist", result.Errors[0].ErrorMessage);
        }
    }
}
