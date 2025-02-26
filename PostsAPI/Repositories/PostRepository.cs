using Microsoft.EntityFrameworkCore;
using PostsAPI.Data;
using PostsAPI.Models;

namespace PostsAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Post> posts, int totalCount)> GetPostsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Posts
                .AsNoTracking()
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id);

            int totalCount = await query.CountAsync();
            var posts = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (posts, totalCount);
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Category) // Incluir categoría
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> UpdatePostAsync(int id, Post updatedPost)
        {
            var existingPost = await _context.Posts.FindAsync(id);
            if (existingPost == null) return null;

            existingPost.Title = updatedPost.Title;
            existingPost.Content = updatedPost.Content;

            await _context.SaveChangesAsync();
            return existingPost;
        }   

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
