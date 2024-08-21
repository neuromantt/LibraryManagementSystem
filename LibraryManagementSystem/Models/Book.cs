using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string? Title { get; set; }
        [MaxLength(100)]
        public string? Author { get; set; }

        public string? Description { get; set; }
        [MaxLength(100)]
        public string? Category { get; set; }
        [MaxLength(100)]
        public string? Publisher { get; set; }
        public int? PublishYear { get; set; }
        [MaxLength(2048)]
        public string? Image { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]
        [MaxLength(450)]
        public string? CreatedById { get; set; }
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]
        [MaxLength(450)]
        public string? LastModifiedById { get; set; }
    }
}
