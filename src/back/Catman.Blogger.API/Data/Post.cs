namespace Catman.Blogger.API.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime LastUpdate { get; set; }
        
        public Guid BlogId { get; set; }
        
        public Blog Blog { get; set; }
    }
}
