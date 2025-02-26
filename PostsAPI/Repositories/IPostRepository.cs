using PostsAPI.Models;

namespace PostsAPI.Repositories
{
    public interface IPostRepository
    {
        Task<(IEnumerable<Post> posts, int totalCount)> GetPostsAsync(int pageNumber, int pageSize);
        Task<Post?> GetPostByIdAsync(int id);
        Task<Post> CreatePostAsync(Post post);
        Task<Post?> UpdatePostAsync(int id, Post updatedPost);
        Task<bool> DeletePostAsync(int id);
    }
}
