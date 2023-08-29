using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Models;

namespace MinimalAPI_Books.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public BookRepository(BookstoreDbContext dbContext)
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

        public async Task AddAsync(Book book, int genreId)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            Language language = await _dbContext.Languages.FindAsync(book.LanguageId);
            Author author = await _dbContext.Authors.FindAsync(book.AuthorId);

            book.Language = language;
            book.Author = author;

            book.Genres.Add(_dbContext.Genres.Find(genreId));

            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _dbContext.Books.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }
    }


}
