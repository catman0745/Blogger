namespace Catman.Blogger.Core.Services.Comment
{
    using System;

    public class CreateCommentRequest
    {
        public string Content { get; set; }
        
        public string OwnerUsername { get; set; }
        
        public Guid PostId { get; set; }
    }
}
