using LibraryManagementSystem.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace LibraryManagementSystem.Models
{
    public class UsersBook
    {
        public int Id { get; set; }
        public UsersBookStatus Status { get; set; } // InWishlist by default
        public string LendTo { get; set; }
        public DateTime AddingDate { get; set; }
        [ForeignKey("ReadingSession")]
        public int ReadingSessionId { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public ReadingSession ReadingSession { get; set; }
        public AppUser User { get; set; }
        public Book Book { get; set; }
    }
}
