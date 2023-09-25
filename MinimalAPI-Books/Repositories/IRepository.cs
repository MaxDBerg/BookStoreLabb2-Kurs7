using FluentValidation;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO;
using MinimalAPI_Books.Models.DTO.BookDTO;

namespace MinimalAPI_Books.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }

}
