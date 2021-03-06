namespace Catman.Blogger.Core.Helpers.Auth
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class AuthOptions : IAuthOptions
    {
        public string Issuer { get; set; }
        
        public string Audience { get; set; }
        
        public int Lifetime { get; set; }
        
        [MinLength(16)]
        public string Key { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
