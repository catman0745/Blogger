namespace Catman.Blogger.Core.Models
{
    using System;
    
    public class Post
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime LastUpdate { get; set; }
        
        public Guid BlogId { get; set; }
    }
}
