namespace Catman.Blogger.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class PostRepository : IPostRepository
    {
        private readonly BloggerDbContext _context;

        public PostRepository(BloggerDbContext context)
        {
            _context = context;
        }
        
        public Task<bool> ExistsAsync(Guid id)
        {
            return _context.Posts.AnyAsync(post => post.Id == id);
        }

        public Task<bool> ExistsAsync(string title, Guid blogId)
        {
            return _context.Posts.AnyAsync(post => post.Title == title && post.BlogId == blogId);
        }

        public Task<Post> GetAsync(Guid id)
        {
            return _context.Posts.AsNoTracking().SingleAsync(post => post.Id == id);
        }

        public Task<Post> GetAsync(string title, Guid blogId)
        {
            return _context.Posts.AsNoTracking().SingleAsync(post => post.Title == title && post.BlogId == blogId);
        }

        public async Task<ICollection<Post>> GetAsync()
        {
            var posts = await _context.Posts.AsNoTracking().ToListAsync();
            return posts;
        }

        public void Add(Post post)
        {
            _context.Posts.Add(post);
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
        }

        public void Remove(Post post)
        {
            _context.Posts.Remove(post);
        }
    }
}
