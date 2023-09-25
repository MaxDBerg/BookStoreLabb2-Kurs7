using MinimalAPI_Books.Models.DTO;

namespace MinimalAPI_Books.Repositories
{
    public interface IBookRepository<T> : IRepository<T> where T : IBookDTO
    {
    }
}
