namespace Catman.Blogger.Data.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class ImageRepository : IImageRepository
    {
        private readonly BloggerDbContext _context;

        public ImageRepository(BloggerDbContext context)
        {
            _context = context;
        }
        
        public Task<bool> ExistsAsync(Guid id)
        {
            return _context.Images.AnyAsync(image => image.Id == id);
        }

        public Task<Image> GetAsync(Guid id)
        {
            return _context.Images.SingleAsync(image => image.Id == id);
        }

        public void Add(Image image)
        {
            _context.Images.Add(image);
        }
    }
}
