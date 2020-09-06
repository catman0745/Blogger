namespace Catman.Blogger.API.DataTransferObjects.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class LoginUserRequestDto
    {
        [Required(ErrorMessage = "username required")]
        [StringLength(25, ErrorMessage = "username cannot be longer than {1} characters")]
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "password required")]
        [StringLength(25, ErrorMessage = "password cannot be longer than {1} characters")]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
