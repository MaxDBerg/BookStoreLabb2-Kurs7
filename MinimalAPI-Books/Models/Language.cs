using System.ComponentModel.DataAnnotations;

namespace MinimalAPI_Books.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
