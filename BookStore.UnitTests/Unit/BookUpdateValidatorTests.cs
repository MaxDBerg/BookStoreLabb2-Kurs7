using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.UnitTests.Factories;
using BookStore.UnitTests.Fixtures;
using MinimalAPI_Books.Data;

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
            var _validator = new BookUpdateValidator(_dbContext);
            var book = BookFactory.CreateBookWithId(9, "test123", "test123desc", 2, 3);

            //act
            var result = await _validator.ValidateAsync(book);

            //assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Book does not exist", result.Errors[0].ErrorMessage);
        }
    }
}
