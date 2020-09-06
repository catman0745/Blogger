namespace Catman.Blogger.Core.Helpers.Auth
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Catman.Blogger.Core.Models;
    using Microsoft.IdentityModel.Tokens;

    public class TokenHelper : ITokenHelper
    {
        private readonly IAuthOptions _options;

        public TokenHelper(IAuthOptions options)
        {
            _options = options;
        }
        
        public string GenerateToken(User user)
        {
            var notBefore = DateTime.UtcNow;
            var expires = DateTime.UtcNow.AddMinutes(_options.Lifetime);

            var signingCredentials =
                new SigningCredentials(_options.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            
            var jwt = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                Claims(user),
                notBefore,
                expires,
                signingCredentials);
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private static IEnumerable<Claim> Claims(User user)
        {
            yield return new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username);
        }
    }
}
