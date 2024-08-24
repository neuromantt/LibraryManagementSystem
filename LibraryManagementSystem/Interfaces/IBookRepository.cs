using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        public Task<IReadOnlyList<Book>> SearchByTitleOrAuthor(string searchString);
    }
}
