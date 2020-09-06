namespace Catman.Blogger.Core.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Catman.Blogger.Core.Services.Common;

    public class BlogService : Service, IBlogService
    {
        private readonly IBlogRepository _blogs;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogs, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _blogs = blogs;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Response<Blog>> CreateAsync(CreateBlogRequest createRequest)
        {
            if (await _blogs.ExistsAsync(createRequest.Name))
            {
                return Failure<Blog>("Blog with such name already exists");
            }

            var blog = _mapper.Map<Blog>(createRequest);
            blog.CreatedAt = DateTime.UtcNow;

            _blogs.Add(blog);
            await _unitOfWork.SaveChangesAsync();

            return Success(blog);
        }

        public async Task<Response<Blog>> GetByIdAsync(Guid id)
        {
            if (!await _blogs.ExistsAsync(id))
            {
                return Failure<Blog>("Blog with such id does not exist");
            }

            var blog = await _blogs.GetAsync(id);
            return Success(blog);
        }

        public async Task<Response<ICollection<Blog>>> GetAllAsync()
        {
            var blogs = await _blogs.GetAsync();
            return Success(blogs);
        }

        public async Task<Response<Blog>> EditAsync(EditBlogRequest editRequest)
        {
            if (!await _blogs.ExistsAsync(editRequest.Id))
            {
                return Failure<Blog>("Blog with such id does not exist");
            }

            if (await _blogs.ExistsAsync(editRequest.Name))
            {
                var blogWithSameName = await _blogs.GetAsync(editRequest.Name);
                
                // allow unchanged blog name
                if (blogWithSameName.Id != editRequest.Id)
                {
                    return Failure<Blog>("Blog with such name already exists");
                }
            }
            
            var blog = await _blogs.GetAsync(editRequest.Id);
            if (blog.OwnerUsername != editRequest.Username)
            {
                return Failure<Blog>("You do not have permission to edit this blog");
            }

            _mapper.Map(editRequest, blog);
            _blogs.Update(blog);
            await _unitOfWork.SaveChangesAsync();

            return Success(blog);
        }

        public async Task<Response<Blog>> DeleteAsync(DeleteBlogRequest deleteRequest)
        {
            if (!await _blogs.ExistsAsync(deleteRequest.Id))
            {
                return Failure<Blog>("Blog with such id does not exist");
            }
            
            var blog = await _blogs.GetAsync(deleteRequest.Id);
            if (blog.OwnerUsername != deleteRequest.Username)
            {
                return Failure<Blog>("You do not have permission to delete this blog");
            }

            _blogs.Remove(blog);
            await _unitOfWork.SaveChangesAsync();

            return Success(blog);
        }
    }
}
