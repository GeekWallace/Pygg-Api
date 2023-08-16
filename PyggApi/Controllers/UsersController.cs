using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PyggApi.Data;
using PyggApi.Interfaces.Lookups;
using PyggApi.Interfaces.Members;
using PyggApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PyggApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IUsers _userBusiness;
        public UsersController(IUsers userBusiness , IConfiguration config)
        {
            _userBusiness = userBusiness;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<List<User>>> AddGroup(User user)
        {
            return await _userBusiness.AddUser(user);
       
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            var currentUser = await _userBusiness.LoginUser(user);
            if (currentUser == null)
            {
                return await Task.FromResult<ActionResult<User>>(NotFound());
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email , user.Email)
           
            };

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(60),
                signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            

            return await Task.FromResult<ActionResult<User>>(new ObjectResult(new
            {
                access_token = jwt,
                token_type = "bearer",
                user_id = currentUser.UserId,
                user_name = currentUser.Name,
                user_phone = currentUser.Phone,
                user_email = currentUser.Email,
                user_password = currentUser.Password,


            }));
        }
    }
}
