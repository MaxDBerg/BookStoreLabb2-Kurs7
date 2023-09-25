using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO;
using MinimalAPI_Books.Models.DTO.BookDTO;
using System.Net;

namespace MinimalAPI_Books.Repositories
{
    public class BookRepository<T> : IBookRepository<T> where T : IBookDTO
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookRepository(BookstoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var bookEntity = await _dbContext.Set<Book>()
                .Include(b => b.Language)
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<T>(bookEntity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var booksEntity = await _dbContext.Set<Book>()
                .Include(b => b.Language)
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .ToListAsync();
            return _mapper.Map<IEnumerable<T>>(booksEntity);
        }

        public async Task AddAsync(T bookDTO)
        {
            var bookDTOEntity = _mapper.Map<BookCreateDTO>(bookDTO);
            var bookEntity = _mapper.Map<Book>(bookDTO);

            // Fetch and associate genres with the book
            var genres = await _dbContext.Genres.Where(g => bookDTOEntity.GenreIds.Contains(g.Id)).ToListAsync();

            // Ensure that all specified genres exist
            if (genres.Count != bookDTOEntity.GenreIds.Count)
            {
                throw new Exception("One or more genres not found."); // Handle this exception appropriately
            }

            // Associate genres with the book
            bookEntity.Genres = genres;

            _dbContext.Attach(bookEntity);
            await _dbContext.Books.AddAsync(bookEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T bookDTO)
        {
            var bookDTOEntity = _mapper.Map<BookUpdateDTO>(bookDTO);

            var existingBook = await _dbContext.Books
                .Include(b => b.Genres) // Include the genres to avoid lazy loading
                .FirstOrDefaultAsync(b => b.Id == bookDTOEntity.Id);

            existingBook.Genres.Clear();

            //var genres = await _dbContext.Genres.Where(g => bookDTO.GenreIds.Contains(g.Id)).ToListAsync();
            //existingBook.Genres = genres;
            var genres = await _dbContext.Genres.Where(g => bookDTOEntity.GenreIds.Contains(g.Id)).ToListAsync();

            existingBook.Genres = genres;

            _mapper.Map(bookDTOEntity, existingBook);

            _dbContext.Books.Entry(existingBook).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _dbContext.Set<Book>().FindAsync(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
