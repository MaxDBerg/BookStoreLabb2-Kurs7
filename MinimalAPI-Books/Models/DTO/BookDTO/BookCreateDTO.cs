using System.ComponentModel.DataAnnotations;

namespace MinimalAPI_Books.Models.DTO.BookDTO
{
    public class BookCreateDTO : IBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "At least one language must be selected.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid language.")]
        public int LanguageId { get; set; }
        [Required(ErrorMessage = "At least one author must be selected.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid author.")]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "At least one genre must be selected.")]
        public List<int> GenreIds { get; set; }
    }
}
