namespace Catman.Blogger.Core.Helpers.Auth
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Catman.Blogger.Core.Helpers.Time;
    using Catman.Blogger.Core.Models;
    using Microsoft.IdentityModel.Tokens;

    public class TokenHelper : ITokenHelper
    {
        private readonly IAuthOptions _options;
        private readonly ITimeHelper _timeHelper;

        public TokenHelper(IAuthOptions options, ITimeHelper timeHelper)
        {
            _options = options;
            _timeHelper = timeHelper;
        }
        
        public string GenerateToken(User user)
        {
            var notBefore = _timeHelper.Now;
            var expires = _timeHelper.Now.AddMinutes(_options.Lifetime);

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
