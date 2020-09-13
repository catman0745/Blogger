namespace Catman.Blogger.API.DataTransferObjects.Comment
{
    using System;
    using System.Text.Json.Serialization;

    public class CommentReadDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("content")]
        public string Content { get; set; }
        
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("owner")]
        public string OwnerUsername { get; set; }
        
        [JsonPropertyName("post")]
        public Guid PostId { get; set; }
    }
}
