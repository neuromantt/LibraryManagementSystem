﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Publisher { get; set; }
        public int? PublishYear { get; set; }
        public string? Image { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]
        public string? CreatedById { get; set; }
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]
        public string? LastModifiedById { get; set; }
    }
}
