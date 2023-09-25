using MinimalAPI_Books.Models.DTO.AuthorDTO;
using MinimalAPI_Books.Models.DTO.LanguageDTO;

namespace MinimalAPI_Books.Models.DTO.BookDTO
{
    public class BookForGenreReadDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public AuthorReadDTO author { get; set; }
        public LanguageReadDTO language { get; set; }
    }
}
