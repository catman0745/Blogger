namespace Catman.Blogger.API.DataTransferObjects.Post
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Newtonsoft.Json;
    
    public class EditPostRequestDto
    {
        [JsonProperty("title")]
        [Required(ErrorMessage = "title required")]
        [StringLength(250, ErrorMessage = "title cannot be longer than {1} characters")]
        public string Title { get; set; }
        
        [JsonPropertyName("content")]
        [Required(ErrorMessage = "content required")]
        public string Content { get; set; }
    }
}
