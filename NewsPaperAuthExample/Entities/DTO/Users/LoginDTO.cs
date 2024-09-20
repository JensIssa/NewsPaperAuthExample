using System.ComponentModel.DataAnnotations;

namespace NewsPaperAuthExample.Entities.DTO.Users
{
    public class LoginDTO
    {
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
