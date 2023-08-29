using MinimalAPI_Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Factories
{
    public static class BookFactory
    {
        public static Book CreateBook(string title, string description, int languageId, int authorId)
        {
            return new Book
            {
                Title = title,
                LanguageId = languageId,
                AuthorId = authorId,
                Description = description
            };
        }
    }
}
