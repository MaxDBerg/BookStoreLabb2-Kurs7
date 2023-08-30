using FluentValidation;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Repositories;
using System.Linq;

namespace MinimalAPI_Books.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        private readonly BookstoreDbContext _dbContext;
        public BookValidator(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required")
                .Must(BeUniqueTitle).WithMessage("Title must be unique");
            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
            RuleFor(book => book.LanguageId).GreaterThan(0).WithMessage("Language is required");
            RuleFor(book => book.AuthorId).GreaterThan(0).WithMessage("Author is required");
        }

        public bool BeUniqueTitle(string title) => !_dbContext.Books.Any(book => book.Title == title);
    }
}
