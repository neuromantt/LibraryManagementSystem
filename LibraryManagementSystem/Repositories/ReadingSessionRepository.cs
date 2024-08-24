using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class ReadingSessionRepository : GenericRepository<ReadingSession>, IReadingSessionRepository
    {
        public ReadingSessionRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }
    }
}
