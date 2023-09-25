using System.Runtime.InteropServices;

namespace MinimalAPI_Books.Models.DTO
{
    public interface IBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
