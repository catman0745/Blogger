namespace Catman.Blogger.Core.Mappings
{
    using AutoMapper;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.User;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterUserRequest, User>();
            CreateMap<User, LoginUserResult>();
        }
    }
}
