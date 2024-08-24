using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class UsersBookRepository : GenericRepository<UsersBook>, IUsersBookRepository
    {
        public UsersBookRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }
    }
}
