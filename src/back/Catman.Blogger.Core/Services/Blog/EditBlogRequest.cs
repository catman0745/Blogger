namespace Catman.Blogger.Core.Services.Blog
{
    using System;

    public class EditBlogRequest
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Username { get; set; }
    }
}
