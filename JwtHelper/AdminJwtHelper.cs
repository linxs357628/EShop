using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShop.JwtHelper
{
    /// <summary>
    /// JWT帮助类
    /// </summary>
    public static class AdminJwtHelper
    {
        //public static readonly string _Issuer = "disiduw";
        //public static readonly string _Audience = "2023";
        //public static readonly string _TockenSecrete = "w2321zxadqqesqq1x31hwp";

        /// <summary>
        /// 生成Tocken
        /// </summary>
        /// <param name="p"></param>
        /// <param name="_Config"></param>
        /// <returns></returns>
        public static string GetTocken(TAdminInfo p, JwtConfig _Config)
        {
            //秘钥
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_Config.Secret)), SecurityAlgorithms.HmacSha256);

            //Claim
            var claims = new Claim[] {
                    new(ClaimTypes.Name,p.AdminName),
                    //new("ChannelId","0"),
                    new("AdminUserID",$"{p.Id}"),
                    new(JwtRegisteredClaimNames.Iss,_Config.Issuer),
                    new(JwtRegisteredClaimNames.Sub, string.Format("admin:{0}", p.Id)),
                    new(_Config.AdminJwtKey, Newtonsoft.Json.JsonConvert.SerializeObject(p)),
            };

            SecurityToken securityToken = new JwtSecurityToken(
                issuer: _Config.Issuer,
                audience: _Config.Audience,
                signingCredentials: securityKey,
                expires: DateTime.Now.AddSeconds(_Config.ExpireTime),//过期时间
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
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

        //public static AdminJwtToken GetAdminInfo(string userJson)
        //{
        //    AdminJwtToken info = Newtonsoft.Json.JsonConvert.DeserializeObject<AdminJwtToken>(userJson);
        //    return info;
        //}
    }
}