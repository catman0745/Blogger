namespace Catman.Blogger.Core.Mappings
{
    using AutoMapper;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Blog;

    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<CreateBlogRequest, Blog>()
                .ForMember(blog => blog.OwnerUsername, options => options.MapFrom(request => request.Username));
            
            CreateMap<EditBlogRequest, Blog>()
                .ForMember(blog => blog.OwnerUsername, options => options.MapFrom(request => request.Username));
        }
    }
}
