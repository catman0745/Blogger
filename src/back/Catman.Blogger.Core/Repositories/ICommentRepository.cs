namespace Catman.Blogger.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;

    public interface ICommentRepository
    {
        Task<ICollection<Comment>> GetByPostIdAsync(Guid postId);
        
        Task<ICollection<Comment>> GetByUsernameAsync(string username);

        Task<bool> ExistsAsync(Guid id);

        Task<Comment> GetAsync(Guid id);

        void Add(Comment comment);
    }
}
