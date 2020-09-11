namespace Catman.Blogger.Core.Services.Image
{
    using Microsoft.AspNetCore.Http;

    public class UploadImageRequest
    {
        public IFormFile Image { get; set; }
        
        public string Username { get; set; }
    }
}
