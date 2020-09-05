namespace Catman.Blogger.API.Mappings
{
    using AutoMapper;
    using Catman.Blogger.API.Data;
    using Catman.Blogger.API.DataTransferObjects.Post;
    
    internal class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<PostCreateDto, Post>();
            CreateMap<PostEditDto, Post>();
            CreateMap<Post, PostReadDto>();
        }
    }
}
