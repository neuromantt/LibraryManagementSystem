using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(350)]
        public string? Title { get; set; }
        [MaxLength(500)]
        public string? Author { get; set; }

        public string? Description { get; set; }
        [MaxLength(100)]
        public string? Category { get; set; }
        public int? PublishYear { get; set; }
        [MaxLength(2048)]
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]
        [MaxLength(450)]
        public string? CreatedById { get; set; }
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]
        [MaxLength(450)]
        public string? LastModifiedById { get; set; }
    }
}
