using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIYer.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_ChatUser_123")]
        public int UserId { get; set; }
        [Required]
        public string MessageText { get; set; }
        public string? FileUrl { get; set; }
        public string? FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }

    }
}
