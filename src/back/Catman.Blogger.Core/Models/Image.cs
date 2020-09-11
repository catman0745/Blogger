namespace Catman.Blogger.Core.Models
{
    using System;

    public class Image
    {
        public Guid Id { get; set; }
        
        public string FileName { get; set; }
        
        public string ContentType { get; set; }
        
        public string OwnerUsername { get; set; }
    }
}
