using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IReadingSessionRepository : IGenericRepository<ReadingSession>
    {
        public Task<IReadOnlyList<ReadingSession>> GetAllByUsersBookId(int usersBookId);
        public Task DeleteAllByUsersBookId(int usersBookId);
        public Task DeleteLastSession(int usersBookId);
        public Task<int> CountAllByUsersBookId(int usersBookId);
        public Task<bool> AlreadyReadPages(int startPage, int endPage, int usersBookId);
        public Task<int> GetLastPageOfUsersBookById(int usersBookId);
    }
}
