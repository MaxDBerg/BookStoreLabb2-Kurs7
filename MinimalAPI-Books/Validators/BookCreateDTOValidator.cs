using FluentValidation;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.BookDTO;
using MinimalAPI_Books.Repositories;
using System.Linq;

namespace MinimalAPI_Books.Validators
{
    public class BookCreateDTOValidator : AbstractValidator<BookCreateDTO>
    {
        private readonly BookstoreDbContext _dbContext;
        public BookCreateDTOValidator(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required")
                .Must(BeUniqueTitle).WithMessage("Title must be unique");
            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
            RuleFor(book => book.LanguageId).GreaterThan(0).WithMessage("LanguageId has to be greater than 0");
            RuleFor(book => book.AuthorId).GreaterThan(0).WithMessage("AuthorId has to be greater than 0");
        }

        public bool BeUniqueTitle(string title) => !_dbContext.Books.Any(book => book.Title == title);
    }
}
