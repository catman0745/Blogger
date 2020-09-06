namespace Catman.Blogger.Core.Mappings
{
    using AutoMapper;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Post;

    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<CreatePostRequest, Post>();
            CreateMap<EditPostRequest, Post>();
        }
    }
}
