using FluentValidation;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using System.Linq;

public class BookUpdateValidator : AbstractValidator<Book>
{
    private readonly BookstoreDbContext _dbContext;

    public BookUpdateValidator(BookstoreDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(book => book).Must(BeExistingBook).WithMessage(book => $"Book with Id: {book.Id} does not exist");
    }

    private bool BeExistingBook(Book book) => _dbContext.Books.Any(dbBook => dbBook.Id == book.Id);
}
