using LibraryManagementSystem.Data.Enum;
using LibraryManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs.UsersBook
{
    public class UsersBookInfoDto
    {
        public int Id { get; set; }
        public UsersBookStatus Status { get; set; }
        public string? LendTo { get; set; }
        public int? BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? BookAuthor { get; set; }
        public string? BookImage { get; set; }
    }
}
