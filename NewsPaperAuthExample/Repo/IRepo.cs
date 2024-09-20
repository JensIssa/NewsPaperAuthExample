using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public interface IRepo
    {
        Article GetArticleById(int id);
        List<Article> GetArticles();
        void CreateArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(int id);
    }
}
