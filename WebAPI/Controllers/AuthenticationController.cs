using Microsoft.AspNetCore.Mvc;
using MyAppAPI.DTOs;
using MyAppAPI.Interface;
using RSA_Angular_.NET_CORE.RSA;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _auth;
        private readonly IRsaHelper _rsaHelper;
        public AuthenticationController(IAuthentication auth, IRsaHelper rsaHelper)
        {
            _auth = auth;
            _rsaHelper = rsaHelper;
        }
        [HttpPost]
        public IActionResult CreateToken(LoginDTO loginInfo)
        {
            loginInfo.Email = _rsaHelper.Decrypt(loginInfo.Email);
            loginInfo.GoogleToken = _rsaHelper.Decrypt(loginInfo.GoogleToken);
            return Ok(_auth.CreateToken(loginInfo));
        }


    }
}
