using NewsPaperAuthExample.Entities.DTO;

namespace NewsPaperAuthExample.Service
{
    public interface ICommentService
    {
        Task<List<CommentDTO>> GetComments();
        Task<List<CommentDTO>> GetCommentByArticleID(int id);
        Task CreateComment(CommentDTO commentDTO);
        Task DeleteComment(int id);
    }
}
