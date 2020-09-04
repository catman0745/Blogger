namespace Catman.Blogger.API.DataTransferObjects.User
{
    using System.Text.Json.Serialization;

    public class UserLoggedDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
