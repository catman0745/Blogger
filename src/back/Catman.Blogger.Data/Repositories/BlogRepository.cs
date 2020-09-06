namespace Catman.Blogger.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class BlogRepository : IBlogRepository
    {
        private readonly BloggerDbContext _context;

        public BlogRepository(BloggerDbContext context)
        {
            _context = context;
        }
        
        public Task<bool> ExistsAsync(Guid id)
        {
            return _context.Blogs.AnyAsync(blog => blog.Id == id);
        }

        public Task<bool> ExistsAsync(string name)
        {
            return _context.Blogs.AnyAsync(blog => blog.Name == name);
        }

        public Task<Blog> GetAsync(Guid id)
        {
            return _context.Blogs.AsNoTracking().SingleAsync(blog => blog.Id == id);
        }

        public Task<Blog> GetAsync(string name)
        {
            return _context.Blogs.AsNoTracking().SingleAsync(blog => blog.Name == name);
        }

        public async Task<ICollection<Blog>> GetAsync()
        {
            var blogs = await _context.Blogs.AsNoTracking().ToListAsync();
            return blogs;
        }

        public void Add(Blog blog)
        {
            _context.Blogs.Add(blog);
        }

        public void Update(Blog blog)
        {
            _context.Blogs.Update(blog);
        }

        public void Remove(Blog blog)
        {
            _context.Blogs.Remove(blog);
        }
    }
}
