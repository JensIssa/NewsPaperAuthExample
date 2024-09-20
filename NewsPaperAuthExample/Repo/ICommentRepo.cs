using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetCommentById(int id);
        Task<List<Comment>> GetComments();
        Task CreateComment(Comment comment);
        Task DeleteComment(int id);
    }
}
