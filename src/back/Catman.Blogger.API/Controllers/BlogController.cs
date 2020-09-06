namespace Catman.Blogger.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.Blog;
    using Catman.Blogger.Core.Services.Blog;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/blogs")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogs;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogs, IMapper mapper)
        {
            _blogs = blogs;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _blogs.GetByIdAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var blog = response.Result;

            var readDto = _mapper.Map<BlogReadDto>(blog);
            return Ok(readDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _blogs.GetAllAsync();
            var blogs = response.Result;

            var readDtos = _mapper.Map<ICollection<BlogReadDto>>(blogs);
            return Ok(readDtos);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogRequestDto createRequestDto)
        {
            var createRequest = _mapper.Map<CreateBlogRequest>(createRequestDto);
            createRequest.Username = User.Identity.Name;

            var response = await _blogs.CreateAsync(createRequest);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var blog = response.Result;

            var readDto = _mapper.Map<BlogReadDto>(blog);
            return StatusCode(StatusCodes.Status201Created, readDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, EditBlogRequestDto editRequestDto)
        {
            var editRequest = _mapper.Map<EditBlogRequest>(editRequestDto);
            editRequest.Id = id;
            editRequest.Username = User.Identity.Name;

            var response = await _blogs.EditAsync(editRequest);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var blog = response.Result;

            var readDto = _mapper.Map<BlogReadDto>(blog);
            return Ok(readDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteRequest = new DeleteBlogRequest()
            {
                Id = id,
                Username = User.Identity.Name
            };

            var response = await _blogs.DeleteAsync(deleteRequest);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var blog = response.Result;

            var readDto = _mapper.Map<BlogReadDto>(blog);
            return Ok(readDto);
        }
    }
}
