namespace Catman.Blogger.Core.Services.Blog
{
    using System;

    public class DeleteBlogRequest
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
    }
}
