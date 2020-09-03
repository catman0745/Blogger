namespace Catman.Blogger.API.Auth
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Catman.Blogger.API.Data;
    using Microsoft.IdentityModel.Tokens;

    public class TokenHelper
    {
        private readonly AuthOptions _options;

        public TokenHelper(AuthOptions options)
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
