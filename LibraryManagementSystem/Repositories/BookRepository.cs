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

        public async Task<IReadOnlyList<Book>> SearchByTitleOrAuthor(string searchString)
        {
            return await _dbContext.Books.Where(s =>
                s.Title!.ToLower().Contains(searchString.ToLower()) ||
                s.Author!.ToLower().Contains(searchString.ToLower())
            ).ToListAsync();
        }
    }
}
