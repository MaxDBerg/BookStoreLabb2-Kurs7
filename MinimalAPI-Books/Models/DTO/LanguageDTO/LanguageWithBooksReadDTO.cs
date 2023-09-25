using MinimalAPI_Books.Models.DTO.BookDTO;

namespace MinimalAPI_Books.Models.DTO.LanguageDTO
{
    public class LanguageWithBooksReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookForLanguageReadDTO> Books { get; set; }
    }
}
