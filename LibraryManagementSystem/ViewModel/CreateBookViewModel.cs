using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModel
{
    public class CreateBookViewModel
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? PublishYear { get; set; }
        public IFormFile? Image { get; set; }
        public string? CreatedById { get; set; }
        public string? LastModifiedById { get; set; }
    }
}
