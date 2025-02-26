using PostsAPI.Models;

namespace PostsAPI.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment> CreateCommentAsync(Comment comment);
    }
}
