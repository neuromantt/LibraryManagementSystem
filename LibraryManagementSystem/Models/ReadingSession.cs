using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class ReadingSession
    {
        public int Id { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public Book Book { get; set; }
        public AppUser User { get; set; }
    }
}
