using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models.DTO.GenreDTO;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.AuthorDTO;

namespace MinimalAPI_Books.Repositories
{
    public class AuthorRepository<T> : IRepository<T>
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorRepository(BookstoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddAsync(T entity)
        {
            var authorEntity = _mapper.Map<Author>(entity);
            await _dbContext.Set<Author>().AddAsync(authorEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var authorEntity = await _dbContext.Set<Author>().FindAsync(id);
            _dbContext.Set<Author>().Remove(authorEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var authorEntity = await _dbContext.Set<Author>()
                .Include(g => g.Books)
                .Include(g => g.Books).ThenInclude(b => b.Genres)
                .Include(g => g.Books).ThenInclude(b => b.Language)
                .ToListAsync();
            return _mapper.Map<IEnumerable<T>>(authorEntity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var authorEntity = await _dbContext.Set<Author>()
                .Include(g => g.Books)
                .Include(g => g.Books).ThenInclude(b => b.Genres)
                .Include(g => g.Books).ThenInclude(b => b.Language)
                .FirstOrDefaultAsync(g => g.Id == id);
            return _mapper.Map<T>(authorEntity);
        }

        public async Task UpdateAsync(T entity)
        {
            var authorEntity = _mapper.Map<Author>(entity);
            _dbContext.Authors.Entry(authorEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
