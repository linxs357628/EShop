using Microsoft.IdentityModel.Tokens;
using Model.Result.Front;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShop.JwtHelper
{
    /// <summary>
    /// JWT帮助类
    /// </summary>
    public class CustomerJwtHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="_Config"></param>
        public static void GetTocken(RM_Customer data, JwtConfig _Config)
        {
            //秘钥
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_Config.Secret)), SecurityAlgorithms.HmacSha256);

            //Claim
            var claims = new Claim[] {
                    new Claim(ClaimTypes.Name,data.User.NickName),
                    new Claim("AdminUserID",$"{data.User.Id}"),
                    new Claim(JwtRegisteredClaimNames.Iss,_Config.Issuer),
                    new Claim(JwtRegisteredClaimNames.Sub, string.Format("customer:{0}", data.User.Id)),
                    new Claim(_Config.CustomerJwtKey, Newtonsoft.Json.JsonConvert.SerializeObject(data.User)),
            };

            data.ExpireAt = DateTime.Now.AddSeconds(_Config.ExpireTime);
            SecurityToken securityToken = new JwtSecurityToken(
                issuer: _Config.Issuer,
                audience: _Config.Audience,
                signingCredentials: securityKey,
                expires: data.ExpireAt,//过期时间
                claims: claims
                );

            data.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        /// <summary>
        /// 获取accessTocken
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetTockenString(HttpContext context)
        {
            return context != null ? context.Request.Headers["Authorization"].ToString() : "";
        }
    }
}