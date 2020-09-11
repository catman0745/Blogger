namespace Catman.Blogger.Core.Helpers.File
{
    using System.Threading.Tasks;

    public interface IFileHelper
    {
        Task SaveAsync(byte[] bytes, string fileName);

        Task<byte[]> GetAsync(string fileName);

        bool IsSupportedImageType(string contentType);
        
        bool IsSupportedImageSize(long size);
    }
}
