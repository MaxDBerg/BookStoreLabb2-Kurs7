using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Models;

namespace MinimalAPI_Books.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly DbContext _dbContext;

        public BookRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Book>().FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Set<Book>().ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            await _dbContext.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            if (book != null)
            {
                await _dbContext.AddAsync(book);
                await _dbContext.SaveChangesAsync();
            }
        }
    }


}
