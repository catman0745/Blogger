namespace Catman.Blogger.API.Mappings
{
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.User;
    using Catman.Blogger.Core.Services.User;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterUserRequestDto, RegisterUserRequest>();
            CreateMap<LoginUserRequestDto, LoginUserRequest>();
            CreateMap<LoginUserResult, LoginUserResultDto>();
        }
    }
}
