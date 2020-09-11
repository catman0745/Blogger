namespace Catman.Blogger.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Image;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _images;

        public ImageController(IImageService images)
        {
            _images = images;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _images.GetAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            var image = response.Result;
            return File(image.Bytes, image.ContentType);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var request = new UploadImageRequest()
            {
                Image = file,
                Username = User.Identity.Name
            };
            
            var response = await _images.UploadAsync(request);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            var id = response.Result;
            return StatusCode(StatusCodes.Status201Created, new { UploadedImageId = id });
        }
    }
}
