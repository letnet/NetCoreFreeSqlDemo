using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace NetCoreFreeSqlDemo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        public LoginController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="s1">用户名</param>
        /// <param name="s2">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        public object Index(string s1,string s2)
        {
            #region 登录逻辑
            (string uid, string role) userInfo = ("1001","user");
            #endregion

            string jwtsecret = _configuration["JWTSecret"].ToString();
            var keyBytes = Encoding.Default.GetBytes(jwtsecret);
            var date = DateTime.Now.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                //Issuer Audience 
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.uid),
                    new Claim(ClaimTypes.Role, userInfo.role),
                    new Claim(ClaimTypes.Expiration, new DateTimeOffset(date).ToUnixTimeMilliseconds().ToString())
                   }),
                Issuer = "UsersCenter",
                Audience = "User",
                Expires = date,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new { error_code = 0, error_msg = "ok", data = tokenString };
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            //登录用户 ClaimTypes.NameIdentifier 信息获取
            var uid = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            return uid;
        }
    }
}
