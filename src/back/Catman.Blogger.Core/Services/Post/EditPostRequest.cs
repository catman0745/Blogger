namespace Catman.Blogger.Core.Services.Post
{
    using System;

    public class EditPostRequest
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public string Username { get; set; }
    }
}
