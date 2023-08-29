using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MinimalAPI_Books.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
