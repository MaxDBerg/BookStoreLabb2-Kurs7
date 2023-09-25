using MinimalAPI_Books.Models.DTO.BookDTO;

namespace MinimalAPI_Books.Models.DTO.GenreDTO
{
    public class GenreWithBooksReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookForGenreReadDTO> Books { get; set; }
    }
}
