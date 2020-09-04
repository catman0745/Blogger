namespace Catman.Blogger.API.DataTransferObjects.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class UserRegisterDto
    {
        [Required(ErrorMessage = "username required")]
        [StringLength(25, ErrorMessage = "username cannot be longer than {1} characters")]
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "fullName required")]
        [StringLength(50, ErrorMessage = "fullName cannot be longer than {1} characters")]
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        
        [Required(ErrorMessage = "password required")]
        [StringLength(25, ErrorMessage = "password cannot be longer than {1} characters")]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
