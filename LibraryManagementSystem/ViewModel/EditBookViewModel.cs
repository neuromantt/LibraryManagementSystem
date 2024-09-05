using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModel
{
    public class EditBookViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? PublishYear { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? LastModifiedById { get; set; }
    }
}
