namespace Catman.Blogger.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class CommentRepository : ICommentRepository
    {
        private readonly BloggerDbContext _context;

        public CommentRepository(BloggerDbContext context)
        {
            _context = context;
        }
        
        public async Task<ICollection<Comment>> GetByPostIdAsync(Guid postId)
        {
            return await _context.Comments.AsNoTracking()
                .Where(comment => comment.PostId == postId)
                .ToListAsync();
        }

        public async Task<ICollection<Comment>> GetByUsernameAsync(string username)
        {
            return await _context.Comments.AsNoTracking()
                .Where(comment => comment.OwnerUsername == username)
                .ToListAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return _context.Comments.AnyAsync(comment => comment.Id == id);
        }

        public Task<Comment> GetAsync(Guid id)
        {
            return _context.Comments.AsNoTracking().SingleAsync(comment => comment.Id == id);
        }

        public void Add(Comment comment)
        {
            _context.Comments.Add(comment);
        }
    }
}
