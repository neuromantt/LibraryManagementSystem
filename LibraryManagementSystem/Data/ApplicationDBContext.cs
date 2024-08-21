using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadingSession> ReadingSessions { get; set; }
        public DbSet<UsersBook> UsersBooks { get; set; }
    }
}
