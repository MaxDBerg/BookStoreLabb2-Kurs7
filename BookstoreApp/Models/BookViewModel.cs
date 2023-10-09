using System.ComponentModel;

namespace BookstoreApp.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [DisplayName("Book Name")]
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? Language { get; set; }
        public string? Author { get; set; }
    }
}
