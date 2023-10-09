using MinimalAPI_Books.Models;

namespace BookstoreApp.Models
{
    public class BookViewCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }
        public List<int> GenreIds { get; set; }
        public List<Genre> genres { get; set; }
    }
}
