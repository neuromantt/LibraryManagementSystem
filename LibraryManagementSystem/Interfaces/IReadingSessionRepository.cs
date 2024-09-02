using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IReadingSessionRepository : IGenericRepository<ReadingSession>
    {
        public Task<IReadOnlyList<ReadingSession>> GetAllByUsersBookId(int usersBookId);
        public Task DeleteAllByUsersBookId(int usersBookId);
    }
}
