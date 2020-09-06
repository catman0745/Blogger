namespace Catman.Blogger.Core.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Common;
    using Microsoft.EntityFrameworkCore;

    public class BlogService : Service, IBlogService
    {
        private readonly BloggerDbContext _context;
        private readonly IMapper _mapper;

        public BlogService(BloggerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Response<Blog>> CreateAsync(CreateBlogRequest createRequest)
        {
            if (await _context.Blogs.AnyAsync(b => b.Name == createRequest.Name))
            {
                return Failure<Blog>("Blog with such name already exists");
            }

            var blog = _mapper.Map<Blog>(createRequest);
            blog.CreatedAt = DateTime.UtcNow;

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Success(blog);
        }

        public async Task<Response<Blog>> GetByIdAsync(Guid id)
        {
            if (!await _context.Blogs.AnyAsync(b => b.Id == id))
            {
                return Failure<Blog>("Blog with such id does not exist");
            }

            var blog = await _context.Blogs.SingleAsync(b => b.Id == id);
            return Success(blog);
        }

        public async Task<Response<ICollection<Blog>>> GetAllAsync()
        {
            ICollection<Blog> blogs = await _context.Blogs.ToListAsync();
            return Success(blogs);
        }

        public async Task<Response<Blog>> EditAsync(EditBlogRequest editRequest)
        {
            if (!await _context.Blogs.AnyAsync(b => b.Id == editRequest.Id))
            {
                return Failure<Blog>("Blog with such id does not exist");
            }

            if (await _context.Blogs.AnyAsync(b => b.Name == editRequest.Name))
            {
                var blogWithSameName = await _context.Blogs.SingleAsync(b => b.Name == editRequest.Name);
                
                // allow unchanged blog name
                if (blogWithSameName.Id != editRequest.Id)
                {
                    return Failure<Blog>("Blog with such name already exists");
                }
            }
            
            var blog = await _context.Blogs.SingleAsync(b => b.Id == editRequest.Id);
            if (blog.OwnerUsername != editRequest.Username)
            {
                return Failure<Blog>("You do not have permission to edit this blog");
            }

            _mapper.Map(editRequest, blog);
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return Success(blog);
        }

        public async Task<Response<Blog>> DeleteAsync(DeleteBlogRequest deleteRequest)
        {
            if (!await _context.Blogs.AnyAsync(b => b.Id == deleteRequest.Id))
            {
                return Failure<Blog>("Blog with such id does not exist");
            }
            
            var blog = await _context.Blogs.SingleAsync(b => b.Id == deleteRequest.Id);
            if (blog.OwnerUsername != deleteRequest.Username)
            {
                return Failure<Blog>("You do not have permission to delete this blog");
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return Success(blog);
        }
    }
}
