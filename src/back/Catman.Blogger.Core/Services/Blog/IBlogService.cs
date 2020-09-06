namespace Catman.Blogger.Core.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Common;

    public interface IBlogService
    {
        Task<Response<Blog>> CreateAsync(CreateBlogRequest createRequest);

        Task<Response<Blog>> GetByIdAsync(Guid id);

        Task<Response<ICollection<Blog>>> GetAllAsync();

        Task<Response<Blog>> EditAsync(EditBlogRequest editRequest);

        Task<Response<Blog>> DeleteAsync(DeleteBlogRequest deleteRequest);
    }
}
