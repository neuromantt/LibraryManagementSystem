using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class UsersBookRepository : GenericRepository<UsersBook>, IUsersBookRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public UsersBookRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<UsersBook>> GetUsersBooks(string userId)
        {
            var id = userId == "" ? null : userId;

            var books = await _dbContext.UsersBooks
                .Where(x => x.UserId == id)
                .Include(x => x.Book)
                .ToListAsync();

            return books;
        }

        public async Task<IReadOnlyList<UsersBook>> GetUsersBooksFromPageWithSize(string userId, int pageIndex, int pageSize)
        {
            var id = userId == "" ? null : userId;

            var books = await _dbContext.UsersBooks
                .Where(x => x.UserId == id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Include(x => x.Book)
                .ToListAsync();

            return books;
        }

        public async Task<IReadOnlyList<UsersBook>> GetUsersBooksByTitleOrAuthorFromPageWithSize(string userId, string searchString, int pageIndex, int pageSize)
        {
            var id = userId == "" ? null : userId;

            var books = await _dbContext.UsersBooks
                .Where(x => x.UserId == id &&
                    (x.Book.Title!.ToLower().Contains(searchString.ToLower()) ||
                    x.Book.Author!.ToLower().Contains(searchString.ToLower()))
                )
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Include(x => x.Book)
                .ToListAsync();

            return books;
        }

        public async Task<int> CountAllUsersBooks(string userId)
        {
            return await _dbContext.UsersBooks
                .Where(x => x.UserId == userId)
                .CountAsync();
        }

        public async Task<int> CountAllUsersBooksSearchByTitleOrAuthor(string userId, string searchString)
        {
            return await _dbContext.UsersBooks
                .Where(x => x.UserId == userId &&
                    (x.Book.Title!.ToLower().Contains(searchString.ToLower()) ||
                    x.Book.Author!.ToLower().Contains(searchString.ToLower()))
                )
                .Include(x => x.Book)
                .CountAsync();
        }
    }
}
