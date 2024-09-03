using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class ReadingSession
    {
        public int Id { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public DateTime AddingDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("UsersBook")]
        public int? UsersBookId { get; set; }
        public UsersBook? UsersBook { get; set; }
    }
}
