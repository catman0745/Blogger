namespace Catman.Blogger.Core.Mappings
{
    using AutoMapper;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Comment;

    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<CreateCommentRequest, Comment>();
        }
    }
}
