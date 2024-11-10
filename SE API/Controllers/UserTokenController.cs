using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SE.Models;
using SE.Models.DTOS;
using SE.Services.Inteface;
using SE_API.Enum;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SE_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserTokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUser _userService;
        public UserTokenController(IConfiguration config, IUser userservice)
        {
            _config = config;
            _userService = userservice;
        }
        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
            if (login?.UserType.ToLower() == "admin" || login?.UserType.ToLower() == "user")
            {
                user = new UserModel
                {
                    UserType = login.UserType,
                };
            }
            return user;
        }

        private string GenerateJsonWebToken(UserModel userInfo)
        {

            var key = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserType),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
};
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel FormatToUserModel(UserModel userData, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                userData.Token = token;
                return userData;
            }
            return null;
        }

        //[AllowAnonymous]
        //[HttpPost]
        //private async Task<UserModel> Login( UserModel login)
        //{
    //    IActionResult response = Unauthorized();
    //    var user = AuthenticateUser(login);
    //    UserModel tokenG = null;

    //        if (user != null)
    //        {
    //            var tokenString = GenerateJsonWebToken(user);
    //    //response = Ok(new { token = tokenString });
    //    response = Ok(FormatToUserModel(login, tokenString));
    //    tokenG = FormatToUserModel(login, tokenString);
    //}
    //        if (tokenG == null)
    //        {
    //            return Ok(new { Messsage = "There is some error response is returning null" });
    //        }
    //        return Ok(tokenG);
//}
[AllowAnonymous]
        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(UserInformation userData)
        {

            var data = await _userService.PostUser(userData);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(UserDTO userData)
        {
            var data = await _userService.getUserLogin(userData);
            if(data != null && data?.User != null && data?.User?.UserType != null)
            {
                RoleEnum role = (RoleEnum)data.User.UserType;
                UserModel login = new UserModel
                {
                    UserType = role.ToString(),
                    UserName = data?.User?.UserName,
                    userId = data.User.Id,
                };

                IActionResult response = Unauthorized();
                var user = AuthenticateUser(login);
                UserModel tokenG = null;

                if (user != null)
                {
                    var tokenString = GenerateJsonWebToken(user);
                    //response = Ok(new { token = tokenString });
                    response = Ok(FormatToUserModel(login, tokenString));
                    tokenG = FormatToUserModel(login, tokenString);
                }
                if (tokenG == null)
                {
                    return Ok(new { Messsage = "There is some error response is returning null" });
                }
                return Ok(tokenG);
            }
            return BadRequest();
        }
    }
}
