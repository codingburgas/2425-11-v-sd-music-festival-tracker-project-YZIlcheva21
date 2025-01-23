using System.ComponentModel.DataAnnotations;

namespace MusicFestivalManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)] // Optional: Limit the length of the username
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)] // Optional: Enforce password length
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)] // Add Email property
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // Optional: For role-based access
    }
}
