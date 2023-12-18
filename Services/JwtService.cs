using EcommerceStore.Models;
using EcommerceStore.Services.Iservices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceStore.Services
{
    public class JwtService : Ijwt
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GenerateJwtToken(User user)
        {
            var secretkey = _config.GetSection("JwtOptions:SecretKey").Value;
            var audience = _config.GetSection("JwtOptions:Audience").Value;
            var issuer = _config.GetSection("JwtOptions:Issuer").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Roles", user.Roles));
            claims.Add( new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred

            };
            var Token = new JwtSecurityTokenHandler().CreateToken(TokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
