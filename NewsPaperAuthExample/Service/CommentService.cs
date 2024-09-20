using AutoMapper;
using NewsPaperAuthExample.Entities;
using NewsPaperAuthExample.Entities.DTO;
using NewsPaperAuthExample.Repo;

namespace NewsPaperAuthExample.Service
{
    public class CommentService : ICommentService

    {
        private readonly ICommentRepo _commentRepo;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepo commentRepo, IMapper mapper1)
        {
            _commentRepo = commentRepo;
            _mapper = mapper1;
        }

        public async Task CreateComment(CommentDTO commentDTO)
        {
            Comment comment = _mapper.Map<Comment>(commentDTO);
            await _commentRepo.CreateComment(comment);
        }

        public async Task DeleteComment(int id)
        {
            await _commentRepo.DeleteComment(id);
        }

        public async   Task<List<CommentDTO>> GetCommentByArticleID(int id)
        {
            var comment = await _commentRepo.GetCommentById(id);  
            return _mapper.Map<List<CommentDTO>>(comment); 
        }

        public async Task<List<CommentDTO>> GetComments()
        {
            var comments = await _commentRepo.GetComments();
            return _mapper.Map<List<CommentDTO>>(comments);
        }
    }
}
