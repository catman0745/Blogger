namespace Catman.Blogger.API.Mappings
{
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.Post;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Post;

    internal class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<CreatePostRequestDto, CreatePostRequest>();
            CreateMap<EditPostRequestDto, EditPostRequest>();
            CreateMap<Post, PostRichReadDto>();
            CreateMap<Post, PostSmallReadDto>();
        }
    }
}
