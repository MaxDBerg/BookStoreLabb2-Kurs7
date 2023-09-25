namespace MinimalAPI_Books.Models.DTO.BookDTO
{
    public class BookUpdateDTO : IBookDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? LanguageId { get; set; }
        public int? AuthorId { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
