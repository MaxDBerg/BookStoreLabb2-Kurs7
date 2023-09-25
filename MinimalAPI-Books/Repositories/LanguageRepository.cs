using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models.DTO.LanguageDTO;
using MinimalAPI_Books.Models;

namespace MinimalAPI_Books.Repositories
{
    public class LanguageRepository<T> : IRepository<T>
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public LanguageRepository(BookstoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddAsync(T entity)
        {
            var authorEntity = _mapper.Map<Language>(entity);
            await _dbContext.Set<Language>().AddAsync(authorEntity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var authorEntity = await _dbContext.Set<Language>().FindAsync(id);
            _dbContext.Set<Language>().Remove(authorEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var authorEntity = await _dbContext.Set<Language>()
                .Include(g => g.Books)
                .Include(g => g.Books).ThenInclude(b => b.Author)
                .Include(g => g.Books).ThenInclude(b => b.Genres)
                .ToListAsync();
            return _mapper.Map<IEnumerable<T>>(authorEntity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var authorEntity = await _dbContext.Set<Language>()
                .Include(g => g.Books)
                .Include(g => g.Books).ThenInclude(b => b.Author)
                .Include(g => g.Books).ThenInclude(b => b.Genres)
                .FirstOrDefaultAsync(g => g.Id == id);
            return _mapper.Map<T>(authorEntity);
        }

        public async Task UpdateAsync(T entity)
        {
            var authorEntity = _mapper.Map<Language>(entity);
            _dbContext.Languages.Entry(authorEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
