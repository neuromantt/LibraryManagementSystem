using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModel
{
    public class BooksViewModel
    {
        public IEnumerable<BookMainInfoDto>? Books { get; set; }
        public string? SearchString { get; set; }
        public Pager? Pager { get; set; }
    }
}
