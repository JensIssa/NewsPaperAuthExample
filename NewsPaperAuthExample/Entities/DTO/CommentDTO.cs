using NewsPaperAuthExample.Entities.DTO.Users;

namespace NewsPaperAuthExample.Entities.DTO
{
    public class CommentDTO
    {
        public string Content { get; set; }

        public UserEditDTO User { get; set; }

    }
}
