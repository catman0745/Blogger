namespace Catman.Blogger.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.API.Data;
    using Catman.Blogger.API.DataTransferObjects.Blog;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/blogs")]
    public class BlogController : ControllerBase
    {
        private readonly BloggerDbContext _context;
        private readonly IMapper _mapper;

        public BlogController(BloggerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            var readDto = _mapper.Map<BlogReadDto>(blog);
            return Ok(readDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _context.Blogs.ToListAsync();

            var readDtos = _mapper.Map<ICollection<BlogReadDto>>(blogs);
            return Ok(readDtos);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateDto createDto)
        {
            if (await _context.Blogs.AnyAsync(b => b.Name == createDto.Name))
            {
                return BadRequest("Blog with such name already exists");
            }

            var blog = _mapper.Map<Blog>(createDto);
            blog.CreatedAt = DateTime.UtcNow;
            blog.OwnerUsername = User.Identity.Name;

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, BlogEditDto editDto)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            if (blog.OwnerUsername != User.Identity.Name)
            {
                return BadRequest("You are not the owner of this blog");
            }

            var otherBlogs = _context.Blogs.Where(b => b.Id != blog.Id);
            if (await otherBlogs.AnyAsync(b => b.Name == editDto.Name && b.Id != blog.Id))
            {
                return BadRequest("Blog name already taken");
            }

            _mapper.Map(editDto, blog);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            if (blog.OwnerUsername != User.Identity.Name)
            {
                return BadRequest("You are not the owner of this blog");
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
