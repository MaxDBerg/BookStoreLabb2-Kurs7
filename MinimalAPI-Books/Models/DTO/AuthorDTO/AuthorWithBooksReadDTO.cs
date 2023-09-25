using MinimalAPI_Books.Models.DTO.BookDTO;

namespace MinimalAPI_Books.Models.DTO.AuthorDTO
{
    public class AuthorWithBooksReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookForAuthorReadDTO> Books { get; set; }
    }
}
