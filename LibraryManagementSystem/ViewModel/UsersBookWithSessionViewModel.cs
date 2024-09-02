using LibraryManagementSystem.DTOs.ReadingSession;
using LibraryManagementSystem.DTOs.UsersBook;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModel
{
    public class UsersBookWithSessionViewModel
    {
        public UsersBookInfoDto UsersBook { get; set; }
        public IEnumerable<ReadingSessionInfoDto>? ReadingSessions { get; set; }
    }
}
