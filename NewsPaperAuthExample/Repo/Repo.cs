using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public class Repo : IRepo
    {
        RepoContext _context;
        public Repo(RepoContext context)
        {
            _context = context;
        }

        public void CreateArticle(Article article)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public void DeleteArticle(int id)
        {
            _context.Articles.Remove(GetArticleById(id));
            _context.SaveChanges();
        }

        public Article GetArticleById(int id)
        {
            return _context.Articles.Find(id) ?? throw new System.Exception("Article not found");
        }

        public List<Article> GetArticles()
        {
            return _context.Articles.ToList();
        }

        public void UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
            _context.SaveChanges();
        }
    }
}
