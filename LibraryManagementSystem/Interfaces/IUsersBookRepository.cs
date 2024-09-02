using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IUsersBookRepository : IGenericRepository<UsersBook>
    {
        public Task<UsersBook> GetUsersBookById(int usersBookId);
        public Task<IReadOnlyList<UsersBook>> GetUsersBooks(string userId);
        public Task<IReadOnlyList<UsersBook>> GetUsersBooksFromPageWithSize(string userId, int pageIndex, int pageSize);
        public Task<IReadOnlyList<UsersBook>> GetUsersBooksByTitleOrAuthorFromPageWithSize(string userId, string searchString, int pageIndex, int pageSize);
        public Task<int> CountAllUsersBooks(string userId);
        public Task<int> CountAllUsersBooksSearchByTitleOrAuthor(string userId, string searchString);
        public Task DeleteUsersBookWithSessions(int usersBookId);
    }
}
