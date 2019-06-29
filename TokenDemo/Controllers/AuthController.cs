using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TokenDemo.Data;
using TokenDemo.Model;

namespace TokenDemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET api/values
        [HttpPost]
        [Route("login")]
        //public async  Task<ActionResult<string>> Login ([FromBody] LoginModel loginModel)
        public ActionResult<string> Login([FromBody] LoginModel loginModel)
        {
            string token = string.Empty;
            //var user =await _userManager.FindByNameAsync(loginModel.UserName);
            //if (user !=null && await _userManager.CheckPasswordAsync(user,loginModel.Password))
            //{
            //    token = GenerateJSONWebToken(loginModel);
            //}

            token = GenerateJSONWebToken(loginModel);

            return token;

            //return _context.Users.Select(p => p.UserName).ToArray();
        }


        public string GenerateJSONWebToken(LoginModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKey"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                //new Claim(JwtRegisteredClaimNames.Email, userInfo.UserName),
                //new Claim("DateOfJoining", userInfo.DateOfJoining.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var token = new JwtSecurityToken(
                issuer:"http://oec.com",
                audience: "http://oec.com",
                expires: DateTime.Now.AddMinutes(120),                
                claims:claims,                
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
