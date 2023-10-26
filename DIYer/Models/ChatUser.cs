using System.ComponentModel.DataAnnotations;

namespace DIYer.Models
{
    public class ChatUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nick { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public double? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
