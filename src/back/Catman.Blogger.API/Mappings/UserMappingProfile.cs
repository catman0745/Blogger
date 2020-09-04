namespace Catman.Blogger.API.Mappings
{
    using AutoMapper;
    using Catman.Blogger.API.Data;
    using Catman.Blogger.API.DataTransferObjects.User;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserLoggedDto>();
        }
    }
}
