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

        public async Task DeleteAllByUsersBookId(int usersBookId)
        {
            var readingSessions = await GetAllByUsersBookId(usersBookId);

            foreach (var item in readingSessions)
            {
                await Delete(item);
            }
        }

        public async Task<IReadOnlyList<ReadingSession>> GetAllByUsersBookId(int usersBookId)
        {
            return await _dbContext.ReadingSessions
                .Where(x => x.UsersBookId == usersBookId)
                .ToListAsync();
        }
    }
}
