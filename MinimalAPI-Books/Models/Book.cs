namespace MinimalAPI_Books.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<string> Genres { get; set; }
        public DateTime Publication { get; set; }
        public bool IsLoanable { get; set; }
    }
}
