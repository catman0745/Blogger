namespace Catman.Blogger.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.API.Data;
    using Catman.Blogger.API.DataTransferObjects.Post;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly BloggerDbContext _context;
        private readonly IMapper _mapper;
    
        public PostController(BloggerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
        
            var readDto = _mapper.Map<PostReadDto>(post);
            return Ok(readDto);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _context.Posts.ToListAsync();
        
            var readDtos = _mapper.Map<ICollection<PostReadDto>>(posts);
            return Ok(readDtos);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateDto createDto)
        {
            var blog = await _context.Blogs
                .Include(b => b.Posts)
                .SingleOrDefaultAsync(b => b.Id == createDto.BlogId);
            if (blog == null)
            {
                return NotFound("Blog with such id does not exist");
            }
            if (blog.OwnerUsername != User.Identity.Name)
            {
                return BadRequest("You are not the owner of that blog");
            }
            
            if (blog.Posts.Any(p => p.Title == createDto.Title))
            {
                return BadRequest("Post with such title already exists in that blog");
            }
            
            var post = _mapper.Map<Post>(createDto);
        
            post.CreatedAt = DateTime.UtcNow;
            post.LastUpdate = post.CreatedAt;
            
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        
            return StatusCode(StatusCodes.Status201Created);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, PostEditDto editDto)
        {
            var post = await _context.Posts
                .Include(p => p.Blog)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
    
            var blog = post.Blog;
            if (blog.OwnerUsername != User.Identity.Name)
            {
                return BadRequest("You are not the owner of this post");
            }

            var otherPostsInBlog = _context.Posts.Where(p => p.BlogId == post.BlogId && p.Id != post.Id);
            if (await otherPostsInBlog.AnyAsync(p => p.Title == editDto.Title))
            {
                return BadRequest("Post with such title already exists in that blog");
            }
        
            _mapper.Map(editDto, post);
            post.LastUpdate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        
            return Ok();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await _context.Posts
                .Include(p => p.Blog)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
    
            var blog = post.Blog;
            if (blog.OwnerUsername != User.Identity.Name)
            {
                return BadRequest("You are not the owner of this post");
            }
        
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        
            return Ok();
        }
    }
}
