namespace Catman.Blogger.Core.Services.Image
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Helpers.File;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Catman.Blogger.Core.Services.Common;
    using Microsoft.AspNetCore.Http;

    public class ImageService : Service, IImageService
    {
        private const int MaxFileSize = 250;
        private const int GuidLength = 36;
        
        private readonly IImageRepository _images;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHelper _fileHelper;

        public ImageService(IImageRepository images, IUnitOfWork unitOfWork, IFileHelper fileHelper)
        {
            _images = images;
            _unitOfWork = unitOfWork;
            _fileHelper = fileHelper;
        }

        public async Task<Response<Guid>> UploadAsync(UploadImageRequest uploadRequest)
        {
            if (!_fileHelper.IsSupportedImageType(uploadRequest.Image.ContentType))
            {
                return Failure<Guid>("Unsupported image type");
            }
            if (!_fileHelper.IsSupportedImageSize(uploadRequest.Image.Length))
            {
                return Failure<Guid>("Image is too large");
            }

            var id = Guid.NewGuid();
            var fileName = $"{id}_{FileName(uploadRequest.Image)}";

            // database
            var image = new Image
            {
                Id = id,
                FileName = fileName,
                ContentType = uploadRequest.Image.ContentType,
                OwnerUsername = uploadRequest.Username
            };
            _images.Add(image);
            await _unitOfWork.SaveChangesAsync();

            // file
            var bytes = await GetFileBytes(uploadRequest.Image);
            await _fileHelper.SaveAsync(bytes, fileName);

            return Success(id);
        }

        public async Task<Response<GetImageResult>> GetAsync(Guid id)
        {
            if (!await _images.ExistsAsync(id))
            {
                return Failure<GetImageResult>("Image does not exist");
            }

            var imageInfo = await _images.GetAsync(id);
            var imageResult = new GetImageResult
            {
                Bytes = await _fileHelper.GetAsync(imageInfo.FileName),
                ContentType = imageInfo.ContentType
            };
            return Success(imageResult);
        }

        private static string FileName(IFormFile file)
        {
            const int maxAllowedLength = MaxFileSize - GuidLength - 1;
            var length = Math.Max(0, file.FileName.Length - maxAllowedLength);
            return file.FileName.Substring(length);
        }

        private static async Task<byte[]> GetFileBytes(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
