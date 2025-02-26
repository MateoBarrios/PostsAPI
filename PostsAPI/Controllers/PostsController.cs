using Microsoft.AspNetCore.Mvc;
using PostsAPI.Models;
using PostsAPI.Repositories;

namespace PostsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (posts, totalCount) = await _postRepository.GetPostsAsync(pageNumber, pageSize);
            return Ok(new { totalCount, pageNumber, pageSize, posts });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] Post post, IFormFile? file)
        {
            if (file != null && file.Length > 0)
            {
            }

            var createdPost = await _postRepository.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post updatedPost)
        {
            var post = await _postRepository.UpdatePostAsync(id, updatedPost);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var deleted = await _postRepository.DeletePostAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
