using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPaperAuthExample.Entities.DTO;
using NewsPaperAuthExample.Service;

namespace NewsPaperAuthExample.Controllers
{
    public class CommentController : ControllerBase
    {
        ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("GetCommentsByArticle")]
        [Authorize(Roles = "Editor,Writer,Subsriber,Guest")]
        public async Task<List<CommentDTO>> GetCommentsByArticle(int articleId)
        {
            return await _commentService.GetCommentByArticleID(articleId);
        }

        [HttpPost("CreateComment")]
        [Authorize(Roles = "Writer,Editor,Subscriber")]
        public async Task CreateComment(CommentDTO commentDTO)
        {
            await _commentService.CreateComment(commentDTO);
        }

        [HttpDelete("DeleteComment")]
        [Authorize(Roles = "Editor")]
        public async Task DeleteComment(int commentId)
        {
            await _commentService.DeleteComment(commentId);
        }
    }
}
