using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IUsersBookRepository : IGenericRepository<UsersBook>
    {
        public Task<IReadOnlyList<UsersBook>> GetUsersBooks(string userId);
        public Task<IReadOnlyList<UsersBook>> GetUsersBooksFromPageWithSize(string userId, int pageIndex, int pageSize);
        public Task<IReadOnlyList<UsersBook>> GetUsersBooksByTitleOrAuthorFromPageWithSize(string userId, string searchString, int pageIndex, int pageSize);
        Task<int> CountAllUsersBooks(string userId);
        Task<int> CountAllUsersBooksSearchByTitleOrAuthor(string userId, string searchString);
    }
}
