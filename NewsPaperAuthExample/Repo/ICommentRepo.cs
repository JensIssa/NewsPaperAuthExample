using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public interface ICommentRepo
    {
        List<Comment> GetCommentById(int id);
        List<Comment> GetComments();
        void CreateComment(Comment comment);
        void DeleteComment(int id);
    }
}
