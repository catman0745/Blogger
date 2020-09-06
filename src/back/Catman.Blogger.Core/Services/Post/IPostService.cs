namespace Catman.Blogger.Core.Services.Post
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Common;

    public interface IPostService
    {
        Task<Response<Post>> CreateAsync(CreatePostRequest createRequest);

        Task<Response<Post>> GetByIdAsync(Guid id);

        Task<Response<ICollection<Post>>> GetAllAsync();

        Task<Response<Post>> EditAsync(EditPostRequest editRequest);

        Task<Response<Post>> DeleteAsync(DeletePostRequest deleteRequest);
    }
}
