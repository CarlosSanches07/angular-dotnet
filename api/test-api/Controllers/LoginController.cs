using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using test_api.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace test_api.Controllers
{
    public class LoginController : Controller
    {
        [Route("api/[controller]")]

        [HttpPost]
        public IActionResult Post([FromBody] dynamic data)
        {
            Person p = null;
            if(data == null){
                return BadRequest();
            }
            if(string.IsNullOrEmpty(data.email.ToString()) || string.IsNullOrEmpty(data.password.ToString()))
                return BadRequest(); 
            p = new Person();
            p.Email = data.email;
            p.Password = data.password;
            if(p.Login()){
                decimal expireMinutes = data.expireMinutes ?? 60 * 10;
                var symmetricKey = Convert.FromBase64String(Startup.SECRET);
                var tokenHandler = new JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = now.AddMinutes(Convert.ToDouble(expireMinutes)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey)
                                                                    , SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(new[]{
                    new Claim("iss", "Teste"),
                    new Claim("aud", "Teste"),
                    new Claim("user-id", Convert.ToString(p.Id))
                })
                };

                var stoken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(stoken);

                var a = new 
                {
                    user = p,
                    token = token
                };
                return Accepted(a);
            }
            else{
                return Unauthorized();
            }
        }
    }
}