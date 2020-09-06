namespace Catman.Blogger.Core.Helpers.Auth
{
    using Microsoft.IdentityModel.Tokens;

    public interface IAuthOptions
    {
        string Issuer { get; }
        
        string Audience { get; }
        
        int Lifetime { get; }
        
        string Key { get; }

        SymmetricSecurityKey SymmetricSecurityKey { get; }
    }
}
