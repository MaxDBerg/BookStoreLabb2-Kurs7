using System.ComponentModel.DataAnnotations;

namespace MinimalAPI_Books.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
