using Microsoft.AspNetCore.Mvc;
using PostsAPI.Models;
using PostsAPI.Repositories;

namespace PostsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentsController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPost(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) return NotFound("Post no encontrado");

            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var post = await _postRepository.GetPostByIdAsync(comment.PostId);
            if (post == null) return NotFound("El post no existe");

            var newComment = await _commentRepository.CreateCommentAsync(comment);
            return Ok(newComment);
        }
    }
}
