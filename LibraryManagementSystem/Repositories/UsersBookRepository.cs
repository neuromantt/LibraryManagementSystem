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
        private readonly IReadingSessionRepository _readingSessionRepository;

        public UsersBookRepository(ApplicationDBContext dbContext, IReadingSessionRepository readingSessionRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            _readingSessionRepository = readingSessionRepository;
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

        public async Task<UsersBook> GetUsersBookById(int usersBookId)
        {
            return await _dbContext.UsersBooks
                .Where(x => x.Id == usersBookId)
                .Include(x => x.Book)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteUsersBookWithSessions(int usersBookId)
        {
            await _readingSessionRepository.DeleteAllByUsersBookId(usersBookId);

            await Delete(await GetById(usersBookId));
        }
    }
}
