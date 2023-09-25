using MinimalAPI_Books.Models.DTO.GenreDTO;
using MinimalAPI_Books.Models.DTO.LanguageDTO;

namespace MinimalAPI_Books.Models.DTO.BookDTO
{
    public class BookForAuthorReadDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public LanguageReadDTO Language { get; set; }
        public List<GenreReadDTO> Genres { get; set; }
    }
}
