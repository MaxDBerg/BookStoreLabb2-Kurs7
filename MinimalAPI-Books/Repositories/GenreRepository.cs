using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.GenreDTO;

namespace MinimalAPI_Books.Repositories
{
    public class GenreRepository<T> : IRepository<T>
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreRepository(BookstoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddAsync(T entity)
        {
            var genreEntity = _mapper.Map<Genre>(entity);
            await _dbContext.Set<Genre>().AddAsync(genreEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genreEntity = await _dbContext.Set<Genre>().FindAsync(id);
            _dbContext.Set<Genre>().Remove(genreEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var genreEntity = await _dbContext.Set<Genre>()
                .Include(g => g.Books)
                .Include(g => g.Books).ThenInclude(b => b.Author)
                .Include(g => g.Books).ThenInclude(b => b.Language)
                .ToListAsync();
            return _mapper.Map<IEnumerable<T>>(genreEntity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var genreEntity = await _dbContext.Set<Genre>()
                .Include(g => g.Books)
                .Include(g => g.Books).ThenInclude(b => b.Author)
                .Include(g => g.Books).ThenInclude(b => b.Language)
                .FirstOrDefaultAsync(g => g.Id == id);
            return _mapper.Map<T>(genreEntity);
        }

        public async Task UpdateAsync(T entity)
        {
            var genreEntity = _mapper.Map<Genre>(entity);
            _dbContext.Genres.Entry(genreEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
