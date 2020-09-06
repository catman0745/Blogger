namespace Catman.Blogger.Core.Models
{
    using System;

    public class Blog
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public string OwnerUsername { get; set; }
        
        public User Owner { get; set; }
    }
}
