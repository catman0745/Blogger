namespace Catman.Blogger.API.DataTransferObjects.Blog
{
    using System;
    using System.Text.Json.Serialization;

    public class BlogReadDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("owner")]
        public string OwnerUsername { get; set; }
    }
}
