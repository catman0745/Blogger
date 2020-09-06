namespace Catman.Blogger.Core.Services.Post
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Helpers.Time;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Common;
    using Microsoft.EntityFrameworkCore;

    public class PostService : Service, IPostService
    {
        private readonly BloggerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITimeHelper _timeHelper;

        public PostService(BloggerDbContext context, IMapper mapper, ITimeHelper timeHelper)
        {
            _context = context;
            _mapper = mapper;
            _timeHelper = timeHelper;
        }
        
        public async Task<Response<Post>> CreateAsync(CreatePostRequest createRequest)
        {
            if (!await _context.Blogs.AnyAsync(b => b.Id == createRequest.BlogId))
            {
                return Failure<Post>("Blog does not exist");
            }
            
            var blog = await _context.Blogs.SingleAsync(b => b.Id == createRequest.BlogId);
            if (blog.OwnerUsername != createRequest.Username)
            {
                return Failure<Post>("You do not have permission to add posts to this blog");
            }
            if (await _context.Posts.AnyAsync(p => p.Title == createRequest.Title && p.BlogId == blog.Id))
            {
                return Failure<Post>("Post with such title already exists");
            }

            var post = _mapper.Map<Post>(createRequest);
            post.CreatedAt = _timeHelper.Now;
            post.LastUpdate = post.CreatedAt;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Success(post);
        }

        public async Task<Response<Post>> GetByIdAsync(Guid id)
        {
            if (!await _context.Posts.AnyAsync(p => p.Id == id))
            {
                return Failure<Post>("Post with such id does not exist");
            }

            var post = await _context.Posts.SingleAsync(p => p.Id == id);
            return Success(post);
        }

        public async Task<Response<ICollection<Post>>> GetAllAsync()
        {
            ICollection<Post> posts = await _context.Posts.ToListAsync();
            return Success(posts);
        }

        public async Task<Response<Post>> EditAsync(EditPostRequest editRequest)
        {
            if (!await _context.Posts.AnyAsync(p => p.Id == editRequest.Id))
            {
                return Failure<Post>("Post with such id doest not exist");
            }
            
            var post = await _context.Posts.SingleAsync(p => p.Id == editRequest.Id);
            var blog = await _context.Blogs.SingleAsync(b => b.Id == post.BlogId);
            if (blog.OwnerUsername != editRequest.Username)
            {
                return Failure<Post>("You do not have permission to edit this post");
            }
            
            if (await _context.Posts.AnyAsync(p => p.Title == editRequest.Title && p.BlogId == blog.Id))
            {
                var postWithSameName =
                    await _context.Posts.SingleAsync(p => p.Title == editRequest.Title && p.Id == blog.Id);
                
                // allow unchanged post title
                if (postWithSameName.Id != editRequest.Id)
                {
                    return Failure<Post>("Post with such title already exists");
                }
            }

            _mapper.Map(editRequest, post);
            post.LastUpdate = _timeHelper.Now;
            
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return Success(post);
        }

        public async Task<Response<Post>> DeleteAsync(DeletePostRequest deleteRequest)
        {
            if (!await _context.Posts.AnyAsync(p => p.Id == deleteRequest.Id))
            {
                return Failure<Post>("Post with such id doest not exist");
            }
            
            var post = await _context.Posts.SingleAsync(p => p.Id == deleteRequest.Id);
            var blog = await _context.Blogs.SingleAsync(b => b.Id == post.BlogId);
            if (blog.OwnerUsername != deleteRequest.Username)
            {
                return Failure<Post>("You do not have permission to delete this post");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Success(post);
        }
    }
}
