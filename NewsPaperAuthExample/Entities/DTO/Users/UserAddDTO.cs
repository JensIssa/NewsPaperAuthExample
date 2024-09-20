using System.ComponentModel.DataAnnotations;

namespace NewsPaperAuthExample.Entities.DTO.Users
{
    public class UserAddDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
