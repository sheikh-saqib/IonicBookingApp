using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.DTOs;
using MyAppAPI.Interface;
using MyAppAPI.Models;
using RSA_Angular_.NET_CORE.RSA;

namespace MyAppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly IAuthentication _auth;
        private readonly IRsaHelper _rsaHelper;
        public UsersController(IDataRepository repo, IAuthentication auth, IRsaHelper rsaHelper)
        {
            _auth = auth;
            _repo = repo;
            _rsaHelper = rsaHelper;
        }
        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<Users>());
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<Users>(id));
        }
        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Users UserUpdateData)
        {
            var existingUserDetails = await _repo.GetById<Users>(UserUpdateData.UserId);
            if (existingUserDetails != null)
            {
                existingUserDetails.FirstName = UserUpdateData.FirstName;
                existingUserDetails.LastName = UserUpdateData.LastName;
                existingUserDetails.Email = UserUpdateData.Email;
                existingUserDetails.Mobile = UserUpdateData.Mobile;
                return Ok(await _repo.Update(existingUserDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDTO signupDTO)
        {
            signupDTO.Email = _rsaHelper.Decrypt(signupDTO.Email);
            signupDTO.GoogleToken = _rsaHelper.Decrypt(signupDTO.GoogleToken);
            var userDetails = new Users
            {
                GoogleToken = signupDTO.GoogleToken,
                FirstName = signupDTO.FirstName,
                LastName = signupDTO.LastName,
                Email = signupDTO.Email,
                Role = signupDTO.Role,
                Mobile = signupDTO.Mobile.ToString()
            };
            await _repo.Create(userDetails);
            return Ok(_auth.CreateToken(signupDTO));
        }



    }
}
