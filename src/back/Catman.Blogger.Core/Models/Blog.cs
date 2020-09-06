namespace Catman.Blogger.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Blog
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        
        [Required]
        public string OwnerUsername { get; set; }
        
        public User Owner { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}
