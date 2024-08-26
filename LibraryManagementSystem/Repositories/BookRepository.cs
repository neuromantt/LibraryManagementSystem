using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public BookRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAllSearchByTitleOrAuthor(string searchString)
        {
            return await _dbContext.Books.Where(s =>
                s.Title!.ToLower().Contains(searchString.ToLower()) ||
                s.Author!.ToLower().Contains(searchString.ToLower())
            ).CountAsync();
        }

        public async Task<IReadOnlyList<Book>> GetAllFromPageWithSize(int pageIndex, int pageSize)
        {
            return await _dbContext.Books.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IReadOnlyList<Book>> GetAllSearchByTitleOrAuthor(string searchString)
        {
            return await _dbContext.Books.Where(s =>
                s.Title!.ToLower().Contains(searchString.ToLower()) ||
                s.Author!.ToLower().Contains(searchString.ToLower())
            ).ToListAsync();
        }

        public async Task<IReadOnlyList<Book>> GetAllSearchByTitleOrAuthorFromPageWithSize(string searchString, int pageIndex, int pageSize)
        {
            return await _dbContext.Books.Where(s =>
                s.Title!.ToLower().Contains(searchString.ToLower()) ||
                s.Author!.ToLower().Contains(searchString.ToLower())
            ).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
