namespace Catman.Blogger.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.Comment;
    using Catman.Blogger.Core.Services.Comment;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _comments;
        private readonly IMapper _mapper;

        public CommentController(ICommentService comments, IMapper mapper)
        {
            _comments = comments;
            _mapper = mapper;
        }
        
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetFromPost(Guid postId)
        {
            var response = await _comments.GetByPostIdAsync(postId);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            var readDtos = _mapper.Map<ICollection<CommentReadDto>>(response.Result);
            return Ok(readDtos);
        }

        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetFromUser(string username)
        {
            var response = await _comments.GetByUsernameAsync(username);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            var readDtos = _mapper.Map<ICollection<CommentReadDto>>(response.Result);
            return Ok(readDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _comments.GetByIdAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            var readDto = _mapper.Map<CommentReadDto>(response.Result);
            return Ok(readDto);
        }

        [Authorize]
        [HttpPost("{postId}")]
        public async Task<IActionResult> Create(Guid postId, CreateCommentRequestDto createRequestDto)
        {
            var request = _mapper.Map<CreateCommentRequest>(createRequestDto);
            request.OwnerUsername = User.Identity.Name;
            request.PostId = postId;

            var response = await _comments.CreateAsync(request);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            var comment = response.Result;
            
            var readDto = _mapper.Map<CommentReadDto>(comment);
            return CreatedAtAction(nameof(Get), new { id = comment.Id }, readDto);
        }
    }
}
