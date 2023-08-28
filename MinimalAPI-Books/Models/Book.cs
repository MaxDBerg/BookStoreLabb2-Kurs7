using System.ComponentModel.DataAnnotations;

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
        public Language Language { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }

    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }

    public class BookGenre
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }

    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }

    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set;}
    }
}
