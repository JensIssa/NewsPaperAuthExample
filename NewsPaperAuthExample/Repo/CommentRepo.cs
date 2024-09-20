using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public class CommentRepo : ICommentRepo
    {
        RepoContext _repoContext;
        public CommentRepo(RepoContext repoContext)
        {
            _repoContext = repoContext;
        }

        public void CreateComment(Comment comment)
        {
            _repoContext.Comments.Add(comment);
            _repoContext.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            Comment comment = _repoContext.Comments.Find(id) ?? throw new System.Exception("Comment not found");
            _repoContext.Comments.Remove(comment);
            _repoContext.SaveChanges();
        }

        public List<Comment> GetCommentById(int ArticleID)
        {
            //return all comments from the article with the given id
            Article article = _repoContext.Articles.Find(ArticleID) ?? throw new System.Exception("Article not found");
            return _repoContext.Comments.Where(c => c.Article.Id == ArticleID).ToList();
        }

        public List<Comment> GetComments()
        {
            return _repoContext.Comments.ToList();
        }
    }
}
