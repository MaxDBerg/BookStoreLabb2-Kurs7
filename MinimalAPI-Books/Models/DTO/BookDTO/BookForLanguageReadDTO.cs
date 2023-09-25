using MinimalAPI_Books.Models.DTO.AuthorDTO;
using MinimalAPI_Books.Models.DTO.GenreDTO;

namespace MinimalAPI_Books.Models.DTO.BookDTO
{
    public class BookForLanguageReadDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public AuthorReadDTO author { get; set; }
        public List<GenreReadDTO> genres { get; set; }
    }
}
