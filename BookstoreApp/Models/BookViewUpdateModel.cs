namespace BookstoreApp.Models
{
    public class BookViewUpdateModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }
        public string GenreIds { get; set; }
    }
}
