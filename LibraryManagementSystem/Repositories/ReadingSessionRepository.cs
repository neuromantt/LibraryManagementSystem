using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class ReadingSessionRepository : GenericRepository<ReadingSession>, IReadingSessionRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ReadingSessionRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AlreadyReadPages(int startPage, int endPage, int usersBookId)
        {
            return await _dbContext.ReadingSessions
                .Where(x => x.UsersBookId == usersBookId &&
                ((x.StartPage <= startPage && startPage <= x.EndPage) ||
                (x.StartPage <= endPage && endPage <= x.EndPage)))
                .AnyAsync();
        }

        public async Task<int> CountAllByUsersBookId(int usersBookId)
        {
            return await _dbContext.ReadingSessions
                .Where(x => x.UsersBookId == usersBookId)
                .CountAsync();
        }

        public async Task DeleteAllByUsersBookId(int usersBookId)
        {
            var readingSessions = await GetAllByUsersBookId(usersBookId);

            foreach (var item in readingSessions)
            {
                await Delete(item);
            }
        }

        public async Task DeleteLastSession(int usersBookId)
        {
            var lastSession = await _dbContext.ReadingSessions
                .Where(x => x.UsersBookId == usersBookId)
                .OrderByDescending(x => x.AddingDate)
                .FirstOrDefaultAsync();
            if (lastSession != null)
            { 
                await Delete(lastSession);
            }
        }

        public async Task<IReadOnlyList<ReadingSession>> GetAllByUsersBookId(int usersBookId)
        {
            return await _dbContext.ReadingSessions
                .Where(x => x.UsersBookId == usersBookId)
                .ToListAsync();
        }

        public async Task<int> GetLastPageOfUsersBookById(int usersBookId)
        {
            if (await CountAllByUsersBookId(usersBookId) == 0)
            {
                return 0;
            }

            return await _dbContext.ReadingSessions
                .Where(x => x.UsersBookId == usersBookId)
                .MaxAsync(x => x.EndPage);
        }
    }
}
