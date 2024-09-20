using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPaperAuthExample.Entities.DTO;
using NewsPaperAuthExample.Entities.DTO.Users;
using NewsPaperAuthExample.Service;

namespace NewsPaperAuthExample.Controllers
{
    public class ArticleController : ControllerBase
    {
        IService _service;
        public ArticleController(IService service) {
            _service = service;
        }

        [HttpGet("GetArticle")]
        [Authorize(Roles = "Editor,Writer,Subsriber,Guest")]
        public async Task<List<ArticleDTO>> ArticleDTOs()
        {
            return await _service.GetArticles();
        }

        [HttpGet("GetArticleById")]
        [Authorize(Roles = "Editor,Writer,Subsriber,Guest")]
        public async Task<ArticleDTO> ArticleDTO(int id)
        {
            return await _service.GetArticleById(id);
        }

        [HttpPost("CreateArticle")]
        [Authorize(Roles = "Writer")]
        public async Task CreateArticle([FromBody] ArticleDTO articleDTO)
        {
            await _service.CreateArticle(articleDTO);
        }

        [HttpPut("UpdateArticle")]
        [Authorize(Roles = "Editor,Writer")]
        public async Task UpdateArticle([FromBody] ArticleDTO articleDTO, [FromBody]UserEditDTO dto)
        {
            await _service.UpdateArticle(articleDTO, dto );
        }

        [HttpDelete("DeleteArticle")]
        [Authorize(Roles = "Editor")]
        public async Task DeleteArticle(int ArticleID, [FromBody] UserEditDTO dto)
        {
            await _service.DeleteArticle(ArticleID);
        }
    }
}
