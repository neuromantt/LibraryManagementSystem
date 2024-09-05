using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        public Task<IReadOnlyList<Book>> GetAllSearchByTitleOrAuthor(string searchString);
        public Task<IReadOnlyList<Book>> GetAllFromPageWithSize(int pageIndex, int pageSize);
        public Task<IReadOnlyList<Book>> GetAllSearchByTitleOrAuthorFromPageWithSize(string searchString, int pageIndex, int pageSize);
        Task<int> CountAllSearchByTitleOrAuthor(string searchString);
        Task<Book> GetByIdNoTracking(int id);
    }
}
