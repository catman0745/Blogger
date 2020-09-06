namespace Catman.Blogger.Core.Services.Post
{
    using System;

    public class CreatePostRequest
    {
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public Guid BlogId { get; set; }
        
        public string Username { get; set; }
    }
}
