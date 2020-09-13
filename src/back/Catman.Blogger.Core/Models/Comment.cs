namespace Catman.Blogger.Core.Models
{
    using System;

    public class Comment
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string OwnerUsername { get; set; }
        
        public Guid PostId { get; set; }
    }
}
