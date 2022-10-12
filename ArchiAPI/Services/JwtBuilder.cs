using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using ArchiAPI.Models;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
namespace ArchiAPI.Services
{
    public sealed class JwtBuilder : IJwtBuilder
    {
        private readonly Config _config;
        private static readonly JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
        public JwtBuilder(IOptions<Config> config)
        {
            _config = config.Value;
        }
        public IEnumerable<Claim> GetClaims(string login,string iconUri)
        {
            return new ClaimsIdentity(new Claim[]
            {
                new Claim("IconURI",iconUri),
                new Claim(ClaimsIdentity.DefaultNameClaimType,login)
            }, "Token").Claims;
        }
        public string Build(string login,string iconUri,TimeSpan timeout)
        {
            var token = new JwtSecurityToken(_config.Issuer, _config.Audience, GetClaims(login,iconUri), DateTime.UtcNow, DateTime.UtcNow.Add(timeout),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key)), "HS256"));
            return _handler.WriteToken(token);
        }
    }
}
