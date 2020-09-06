namespace Catman.Blogger.Core.Services.Post
{
    using System;

    public class DeletePostRequest
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
    }
}
