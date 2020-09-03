namespace Catman.Blogger.API.Data
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        [Required]
        [StringLength(25)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        
        [Required]
        [StringLength(25)]
        public string Password { get; set; }
    }
}
