using System.ComponentModel.DataAnnotations;

namespace NewsPaperAuthExample.Entities.DTO.Users
{
    public class UserEditDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }
}
