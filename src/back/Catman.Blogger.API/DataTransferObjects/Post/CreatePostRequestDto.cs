namespace Catman.Blogger.API.DataTransferObjects.Post
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    
    public class CreatePostRequestDto
    {
        [JsonPropertyName("title")]
        [Required(ErrorMessage = "title required")]
        [StringLength(250, ErrorMessage = "title cannot be longer than {1} characters")]
        public string Title { get; set; }
        
        [JsonPropertyName("content")]
        [Required(ErrorMessage = "content required")]
        public string Content { get; set; }
        
        [JsonPropertyName("blogId")]
        [NotEmpty(ErrorMessage = "blogId required")]
        public Guid BlogId { get; set; }
    }
}
