namespace Catman.Blogger.API.DataTransferObjects.Post
{
    using System;
    using System.Text.Json.Serialization;
    
    public class PostRichReadDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("content")]
        public string Content { get; set; }
    
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("lastUpdate")]
        public DateTime LastUpdate { get; set; }
        
        [JsonPropertyName("views")]
        public int ViewsCount { get; set; }
        
        [JsonPropertyName("blogId")]
        public Guid BlogId { get; set; }
    }
}
