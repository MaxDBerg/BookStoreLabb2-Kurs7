using MinimalAPI_Books.Models.DTO.AuthorDTO;
using MinimalAPI_Books.Models.DTO.GenreDTO;
using MinimalAPI_Books.Models.DTO.LanguageDTO;

namespace MinimalAPI_Books.Models.DTO.BookDTO
{
    public class BookReadDTO : IBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LanguageReadDTO Language { get; set; }
        public AuthorReadDTO Author { get; set; }
        public List<GenreReadDTO> Genres { get; set; }
    }
}
