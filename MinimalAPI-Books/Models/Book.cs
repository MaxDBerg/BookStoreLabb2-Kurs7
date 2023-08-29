using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MinimalAPI_Books.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Publication { get; set; }
        public bool IsLoanable { get; set; }

        public int LanguageId { get; set; }
        [JsonIgnore]
        public Language Language { get; set; }

        public int AuthorId { get; set; }
        [JsonIgnore]
        public Author Author { get; set; }
        [JsonIgnore]
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
