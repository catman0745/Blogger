namespace Catman.Blogger.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;

    public interface IBlogRepository
    {
        Task<bool> ExistsAsync(Guid id);
        
        Task<bool> ExistsAsync(string name);

        Task<Blog> GetAsync(Guid id);
        
        Task<Blog> GetAsync(string name);

        Task<ICollection<Blog>> GetAsync();
        
        void Add(Blog blog);

        void Update(Blog blog);

        void Remove(Blog blog);
    }
}
