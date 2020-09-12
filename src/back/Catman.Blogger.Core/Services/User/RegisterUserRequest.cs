namespace Catman.Blogger.Core.Services.User
{
    public class RegisterUserRequest
    {
        public string Username { get; set; }
        
        public string FullName { get; set; }
        
        public string Password { get; set; }
        
        public string AvatarUrl { get; set; }
    }
}
