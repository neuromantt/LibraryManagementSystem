using LibraryManagementSystem.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace LibraryManagementSystem.Models
{
    public class UsersBook
    {
        public int Id { get; set; }
        public UsersBookStatus Status { get; set; } = UsersBookStatus.InWishlist;
        [MaxLength(450)]
        public string LendTo { get; set; } = string.Empty;
        public DateTime AddingDate { get; set; } = DateTime.Now;
        [ForeignKey("AppUser")]
        [MaxLength(450)]
        public string? UserId { get; set; }
        [ForeignKey("Book")]
        public int? BookId { get; set; }
        public AppUser? User { get; set; }
        public Book? Book { get; set; }
    }
}
