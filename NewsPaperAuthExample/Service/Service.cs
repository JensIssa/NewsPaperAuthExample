using AutoMapper;
using NewsPaperAuthExample.Entities;
using NewsPaperAuthExample.Entities.DTO;
using NewsPaperAuthExample.Entities.DTO.Users;
using NewsPaperAuthExample.Repo;

namespace NewsPaperAuthExample.Service
{
    public class Service : IService
    {
        private readonly IRepo _repo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public Service(IRepo repo, IUserRepo userRepo, IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public  Task CreateArticle(ArticleDTO articleDTO)
        {
            Article article = _mapper.Map<Article>(articleDTO);
            _repo.CreateArticle(article);
            return Task.CompletedTask;
        }

        public Task DeleteArticle(int id)
        {
            _repo.DeleteArticle(id);
            return Task.CompletedTask;
        }

        public Task<ArticleDTO> GetArticleById(int id)
        {
            Article article = _repo.GetArticleById(id);
            return Task.FromResult(_mapper.Map<ArticleDTO>(article));
        }

        public Task<List<ArticleDTO>> GetArticles()
        {
            List<Article> articles = _repo.GetArticles();
            return Task.FromResult(_mapper.Map<List<ArticleDTO>>(articles));
        }

        public Task UpdateArticle(ArticleDTO articleDTO, UserEditDTO dto)
        {
            //get roles of user
            User user = _mapper.Map<User>(dto);
            List<string> roles = _userRepo.GetRolesAsync(user).Result;

            //check if user has permission to update article
            if (roles.Contains("Editor") || roles.Contains("Writer"))
            {
                Article article = _mapper.Map<Article>(articleDTO);
                _repo.UpdateArticle(article);
                return Task.CompletedTask;
            }
            else
            {
                throw new UnauthorizedAccessException("User does not have permission to update article");
            }
        }
    }
}