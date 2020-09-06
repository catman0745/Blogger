namespace Catman.Blogger.Core.Services.Post
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Helpers.Time;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Catman.Blogger.Core.Services.Common;

    public class PostService : Service, IPostService
    {
        private readonly IPostRepository _posts;
        private readonly IBlogRepository _blogs;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITimeHelper _timeHelper;

        public PostService(
            IPostRepository posts,
            IBlogRepository blogs,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITimeHelper timeHelper)
        {
            _posts = posts;
            _blogs = blogs;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _timeHelper = timeHelper;
        }
        
        public async Task<Response<Post>> CreateAsync(CreatePostRequest createRequest)
        {
            if (!await _blogs.ExistsAsync(createRequest.BlogId))
            {
                return Failure<Post>("Blog does not exist");
            }
            
            var blog = await _blogs.GetAsync(createRequest.BlogId);
            if (blog.OwnerUsername != createRequest.Username)
            {
                return Failure<Post>("You do not have permission to add posts to this blog");
            }
            if (await _posts.ExistsAsync(createRequest.Title, blog.Id))
            {
                return Failure<Post>("Post with such title already exists");
            }

            var post = _mapper.Map<Post>(createRequest);
            post.CreatedAt = _timeHelper.Now;
            post.LastUpdate = post.CreatedAt;

            _posts.Add(post);
            await _unitOfWork.SaveChangesAsync();

            return Success(post);
        }

        public async Task<Response<Post>> GetByIdAsync(Guid id)
        {
            if (!await _posts.ExistsAsync(id))
            {
                return Failure<Post>("Post with such id does not exist");
            }

            var post = await _posts.GetAsync(id);
            return Success(post);
        }

        public async Task<Response<ICollection<Post>>> GetAllAsync()
        {
            var posts = await _posts.GetAsync();
            return Success(posts);
        }

        public async Task<Response<Post>> EditAsync(EditPostRequest editRequest)
        {
            if (!await _posts.ExistsAsync(editRequest.Id))
            {
                return Failure<Post>("Post with such id does not exist");
            }
            
            var post = await _posts.GetAsync(editRequest.Id);
            var blog = await _blogs.GetAsync(post.BlogId);
            if (blog.OwnerUsername != editRequest.Username)
            {
                return Failure<Post>("You do not have permission to edit this post");
            }
            
            if (await _posts.ExistsAsync(editRequest.Title, blog.Id))
            {
                var postWithSameName = await _posts.GetAsync(editRequest.Title, blog.Id);
                
                // allow unchanged post title
                if (postWithSameName.Id != editRequest.Id)
                {
                    return Failure<Post>("Post with such title already exists");
                }
            }

            _mapper.Map(editRequest, post);
            post.LastUpdate = _timeHelper.Now;
            
            _posts.Update(post);
            await _unitOfWork.SaveChangesAsync();

            return Success(post);
        }

        public async Task<Response<Post>> DeleteAsync(DeletePostRequest deleteRequest)
        {
            if (!await _posts.ExistsAsync(deleteRequest.Id))
            {
                return Failure<Post>("Post with such id does not exist");
            }
            
            var post = await _posts.GetAsync(deleteRequest.Id);
            var blog = await _blogs.GetAsync(post.BlogId);
            if (blog.OwnerUsername != deleteRequest.Username)
            {
                return Failure<Post>("You do not have permission to delete this post");
            }

            _posts.Remove(post);
            await _unitOfWork.SaveChangesAsync();

            return Success(post);
        }
    }
}
