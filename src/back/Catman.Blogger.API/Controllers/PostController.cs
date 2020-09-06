namespace Catman.Blogger.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.Post;
    using Catman.Blogger.Core.Services.Post;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _posts;
        private readonly IMapper _mapper;
    
        public PostController(IPostService posts, IMapper mapper)
        {
            _posts = posts;
            _mapper = mapper;
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _posts.GetByIdAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var post = response.Result;

            var readDto = _mapper.Map<PostReadDto>(post);
            return Ok(readDto);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _posts.GetAllAsync();
            var posts = response.Result;

            var readDtos = _mapper.Map<ICollection<PostReadDto>>(posts);
            return Ok(readDtos);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostRequestDto createRequestDto)
        {
            var createRequest = _mapper.Map<CreatePostRequest>(createRequestDto);
            createRequest.Username = User.Identity.Name;

            var response = await _posts.CreateAsync(createRequest);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var post = response.Result;

            var readDto = _mapper.Map<PostReadDto>(post);
            return StatusCode(StatusCodes.Status201Created, readDto);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, EditPostRequestDto editRequestDto)
        {
            var editRequest = _mapper.Map<EditPostRequest>(editRequestDto);
            editRequest.Id = id;
            editRequest.Username = User.Identity.Name;

            var response = await _posts.EditAsync(editRequest);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var post = response.Result;

            var readDto = _mapper.Map<PostReadDto>(post);
            return Ok(readDto);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteRequest = new DeletePostRequest()
            {
                Id = id,
                Username = User.Identity.Name
            };

            var response = await _posts.DeleteAsync(deleteRequest);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var post = response.Result;

            var readDto = _mapper.Map<PostReadDto>(post);
            return Ok(readDto);
        }
    }
}
