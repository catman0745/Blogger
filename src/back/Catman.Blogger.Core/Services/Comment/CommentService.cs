namespace Catman.Blogger.Core.Services.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Helpers.Time;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Catman.Blogger.Core.Services.Common;

    public class CommentService : Service, ICommentService
    {
        private readonly ICommentRepository _comments;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeHelper _timeHelper;
        private readonly IMapper _mapper;

        public CommentService(
            ICommentRepository comments, 
            IUnitOfWork unitOfWork, 
            ITimeHelper timeHelper, 
            IMapper mapper)
        {
            _comments = comments;
            _unitOfWork = unitOfWork;
            _timeHelper = timeHelper;
            _mapper = mapper;
        }
        
        public async Task<Response<ICollection<Comment>>> GetByPostIdAsync(Guid postId)
        {
            var postComments = await _comments.GetByPostIdAsync(postId);
            return Success(postComments);
        }

        public async Task<Response<ICollection<Comment>>> GetByUsernameAsync(string username)
        {
            var userComments = await _comments.GetByUsernameAsync(username);
            return Success(userComments);
        }

        public async Task<Response<Comment>> GetByIdAsync(Guid id)
        {
            if (!await _comments.ExistsAsync(id))
            {
                return Failure<Comment>("Comment with such id does not exist");
            }

            var comment = await _comments.GetAsync(id);
            return Success(comment);
        }

        public async Task<Response<Comment>> CreateAsync(CreateCommentRequest createRequest)
        {
            var comment = _mapper.Map<Comment>(createRequest);
            comment.CreatedAt = _timeHelper.Now;

            _comments.Add(comment);
            await _unitOfWork.SaveChangesAsync();

            return Success(comment);
        }
    }
}
