namespace MovieTicketAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AuthRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
