namespace Catman.Blogger.API.Mappings
{
    using AutoMapper;
    using Catman.Blogger.API.Data;
    using Catman.Blogger.API.DataTransferObjects.Blog;

    internal class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<BlogCreateDto, Blog>();
            CreateMap<BlogEditDto, Blog>();
            CreateMap<Blog, BlogReadDto>();
        }
    }
}
