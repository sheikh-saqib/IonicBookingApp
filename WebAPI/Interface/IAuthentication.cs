using Microsoft.IdentityModel.Tokens;
using MyAppAPI.DTOs;
using MyAppAPI.Models;

namespace MyAppAPI.Interface
{
    public interface IAuthentication
    {
        string CreateToken(LoginDTO loginInfo);
    }
}
