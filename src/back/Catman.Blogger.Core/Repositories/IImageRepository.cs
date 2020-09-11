namespace Catman.Blogger.Core.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;

    public interface IImageRepository
    {
        Task<bool> ExistsAsync(Guid id);

        Task<Image> GetAsync(Guid id);
        
        void Add(Image image);
    }
}
