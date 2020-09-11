namespace Catman.Blogger.Core.Helpers.File
{
    using System.IO;
    using System.Threading.Tasks;

    public class FileHelper : IFileHelper
    {
        private readonly IFileOptions _options;

        public FileHelper(IFileOptions options)
        {
            _options = options;
        }
        
        public Task SaveAsync(byte[] bytes, string fileName)
        {
            return File.WriteAllBytesAsync(PathToFile(fileName), bytes);
        }

        public Task<byte[]> GetAsync(string fileName)
        {
            return File.ReadAllBytesAsync(PathToFile(fileName));
        }

        public bool IsSupportedImageType(string contentType)
        {
            return _options.SupportedImageTypes.Contains(contentType);
        }

        public bool IsSupportedImageSize(long size)
        {
            return _options.MaxImageSize >= size;
        }

        private string PathToFile(string fileName)
        {
            var uploadsPath = _options.UploadsDirectoryPath;
            if (!uploadsPath.EndsWith(Path.DirectorySeparatorChar))
            {
                uploadsPath += Path.DirectorySeparatorChar;
            }
            
            return uploadsPath + fileName;
        }
    }
}
