namespace Catman.Blogger.Core.Services.Image
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Common;

    public interface IImageService
    {
        Task<Response<Guid>> UploadAsync(UploadImageRequest uploadRequest);

        Task<Response<GetImageResult>> GetAsync(Guid id);
    }
}
