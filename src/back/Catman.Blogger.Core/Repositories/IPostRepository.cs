namespace Catman.Blogger.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;

    public interface IPostRepository
    {
        Task<bool> ExistsAsync(Guid id);
        
        Task<bool> ExistsAsync(string title, Guid blogId);
        
        Task<Post> GetAsync(Guid id);
        
        Task<Post> GetAsync(string title, Guid blogId);

        Task<ICollection<Post>> GetAsync();
        
        void Add(Post post);
        
        void Update(Post post);
        
        void Remove(Post post);
    }
}
