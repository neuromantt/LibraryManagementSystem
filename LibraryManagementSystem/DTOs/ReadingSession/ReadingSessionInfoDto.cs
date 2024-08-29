using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.DTOs.ReadingSession
{
    public class ReadingSessionInfoDto
    {
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
