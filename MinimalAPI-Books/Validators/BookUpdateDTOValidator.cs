using FluentValidation;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.BookDTO;
using System.Linq;

namespace MinimalAPI_Books.Validators
{
    public class BookUpdateDTOValidator : AbstractValidator<BookUpdateDTO>
    {
        private readonly BookstoreDbContext _dbContext;

        public BookUpdateDTOValidator(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters");
            RuleFor(book => book)
                .Must(BeUniqueTitle).WithMessage("Title must be unique");
            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
            RuleFor(book => book).Must(BeExistingBook).WithMessage(book => $"Book with Id: {book.Id} does not exist");
        }

        private bool BeExistingBook(BookUpdateDTO book) => _dbContext.Books.Any(dbBook => dbBook.Id == book.Id);
        public bool BeUniqueTitle(BookUpdateDTO bookDTO)
        {
            return !_dbContext.Books.Where(book => book.Id != bookDTO.Id).Any(book => book.Title == bookDTO.Title);
        }
    }
}