using NewsPaperAuthExample.Entities.DTO;
using NewsPaperAuthExample.Entities.DTO.Users;

namespace NewsPaperAuthExample.Service
{
    public interface IService
    {
        Task<List<ArticleDTO>> GetArticles();
        Task<ArticleDTO> GetArticleById(int id);
        Task CreateArticle(ArticleDTO articleDTO);
        Task UpdateArticle(ArticleDTO articleDTO, UserEditDTO dto);
        Task DeleteArticle(int id);
    }
}
