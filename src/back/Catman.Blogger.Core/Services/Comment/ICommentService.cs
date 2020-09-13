namespace Catman.Blogger.Core.Services.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Common;

    public interface ICommentService
    {
        Task<Response<ICollection<Comment>>> GetByPostIdAsync(Guid postId);
        
        Task<Response<ICollection<Comment>>> GetByUsernameAsync(string username);

        Task<Response<Comment>> GetByIdAsync(Guid id);

        Task<Response<Comment>> CreateAsync(CreateCommentRequest createRequest);
    }
}
