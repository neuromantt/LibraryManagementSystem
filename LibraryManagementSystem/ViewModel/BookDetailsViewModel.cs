using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModel
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public bool CanBeAdded { get; set; }
    }
}
