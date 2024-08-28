using LibraryManagementSystem.DTOs.UsersBook;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModel
{
    public class UsersBooksViewModel
    {
        public IEnumerable<UsersBookInfoDto>? Books { get; set; }
        public string? SearchString { get; set; }
        public Pager? Pager { get; set; }
    }
}
