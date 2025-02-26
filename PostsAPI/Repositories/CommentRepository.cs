using Microsoft.EntityFrameworkCore;
using PostsAPI.Data;
using PostsAPI.Models;

namespace PostsAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments.AsNoTracking().Where(c => c.PostId == postId).OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
