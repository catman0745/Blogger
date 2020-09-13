namespace Catman.Blogger.API.DataTransferObjects.Comment
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class CreateCommentRequestDto
    {
        [JsonPropertyName("content")]
        [Required(ErrorMessage = "content required")]
        [StringLength(500, ErrorMessage = "content cannot be longer than {1} characters")]
        public string Content { get; set; }
    }
}
