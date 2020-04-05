using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SftLib.Data.Domain.Models;
using SftLibrary.API.Models;
using SftLibrary.API.Resources;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SftLibrary.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IConfiguration config, IMapper mapper, UserManager<User> usermanager, SignInManager<User> signInManager)
        {
            _config = config;
            _mapper = mapper;
            _usermanager = usermanager;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterResource userForRegister)
        {
            var userToCreate = _mapper.Map<User>(userForRegister);

            var result = await _usermanager.CreateAsync(userToCreate, userForRegister.Password);

            if (result.Succeeded)
            {
                //Created the role
                _usermanager.AddToRoleAsync(userToCreate,"Member").Wait();
                var userToReturn = _mapper.Map<UserForRegisterResource>(userToCreate);
                return Ok(userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginResource userForLogin)
        {
            var user = await _usermanager.FindByNameAsync(userForLogin.UserName.ToUpper());

            if (user == null)
                return BadRequest("Faild to find Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserForListResource>(user);

                return Ok(new
                {
                    token = GenerateJwtToken(user).Result,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        #region Generate Token

        private async Task<string> GenerateJwtToken(User user)
        {
            //Make the list of claim which we want to add into the token 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            //Get the roles and dump into the claim list
            var roles = await _usermanager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Created Security key
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
