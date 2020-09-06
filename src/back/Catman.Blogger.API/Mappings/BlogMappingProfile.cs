namespace Catman.Blogger.API.Mappings
{
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.Blog;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Blog;

    internal class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<CreateBlogRequestDto, CreateBlogRequest>();
            CreateMap<EditBlogRequestDto, EditBlogRequest>();
            CreateMap<Blog, BlogReadDto>();
        }
    }
}
