namespace Catman.Blogger.API.DataTransferObjects.Blog
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class BlogCreateDto
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "name required")]
        [StringLength(100, ErrorMessage = "name cannot be longer than {1} characters")]
        public string Name { get; set; }
    }
}
