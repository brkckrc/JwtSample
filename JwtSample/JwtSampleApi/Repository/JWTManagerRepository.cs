using JwtSampleApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtSampleApi.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;
        public JWTManagerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            {"user1","password1" },
            {"user2","password2" },
            {"user3","password3" }
        };
        public Tokens Authenticate(Users user)
        {
            if (!UserRecords.Any(a => a.Key == user.Name && a.Value == user.Password))
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, user.Name)
                        }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new Tokens { Token = tokenHandler.WriteToken(token) };
            }
        }
    }
}
