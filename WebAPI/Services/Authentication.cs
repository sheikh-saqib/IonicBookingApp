using Microsoft.IdentityModel.Tokens;
using MyAppAPI.Context;
using MyAppAPI.DTOs;
using MyAppAPI.Interface;
using MyAppAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAppAPI.Services
{
    public class Authentication : IAuthentication
    {
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;
        public Authentication(IConfiguration config, DataContext context)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _context = context;
        }
        public string CreateToken(LoginDTO loginInfo)
        {
            Users userDetails = _context.Users.Where(x => x.Email == loginInfo.Email && x.GoogleToken == loginInfo.GoogleToken).FirstOrDefault();

            if (userDetails != null)
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,userDetails.Email),
                new Claim(ClaimTypes.Role,userDetails.Role),
                new Claim(ClaimTypes.NameIdentifier,userDetails.FirstName+' '+ userDetails.LastName),
                new Claim(ClaimTypes.MobilePhone,userDetails.Mobile),
                new Claim("UserId",userDetails.UserId.ToString()),
            };
                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var res = tokenHandler.WriteToken(token);
                return res;

            }
            else
            {
                return "";
            }
        }
    }
}
