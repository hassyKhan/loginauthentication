using LoginAuth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace LoginAuth.Services
{
    public class Auth
    {
        private readonly IConfiguration _config;
        public Auth(IConfiguration config)
        {
            _config = config;
        }
        LoginRequest user = null;
        public LoginRequest AutheticationUser(LoginRequest login)
        {
            if (login.Username.ToLower() == "husnain")
            {
                user = new LoginRequest
                {
                    Username = login.Username,
                    Password = login.Password,
                };
            }
            return user;
        }

        public string GenerateJSONWebToken(LoginRequest userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Issuer"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now .AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
