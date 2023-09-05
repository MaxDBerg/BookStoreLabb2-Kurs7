using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Factories
{
    public static class BookFactory
    {
        public static Book CreateBookWithId(int id, string title, string description, int languageId, int authorId)
        {
            return new Book
            {
                Id = id,
                Title = title,
                LanguageId = languageId,
                AuthorId = authorId,
                Description = description
            };
        }
        public static BookCreateDTO CreateBook_DTO(string title, string description, int languageId, int authorId, List<int> genreIds)
        {
            return new BookCreateDTO
            {
                Title = title,
                LanguageId = languageId,
                AuthorId = authorId,
                Description = description,
                GenreId = genreIds
            };
        }
        public static BookUpdateDTO UpdateBookWithId_DTO(int id, string title, string description, int languageId, int authorId, List<int> genreIds)
        {
            return new BookUpdateDTO
            {
                Id = id,
                Title = title,
                Description = description,
                LanguageId = languageId,
                AuthorId = authorId,
                GenreIds = genreIds
            };
        }
    }
}
