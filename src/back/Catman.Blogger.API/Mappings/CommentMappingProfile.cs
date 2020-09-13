namespace Catman.Blogger.API.Mappings
{
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.Comment;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Comment;

    internal class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentReadDto>();
            CreateMap<CreateCommentRequestDto, CreateCommentRequest>();
        }
    }
}
