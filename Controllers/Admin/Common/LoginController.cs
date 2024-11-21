using EShop.JwtHelper;
using Model.Parameter.Admin;
using Model.Result.Admin;

namespace EShop.Controllers.Admin.Common
{
    /// <summary>
    /// 登录
    /// </summary>

    [Route("api/admin[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IService.Admin.IAdminService _Service;

        private readonly IOptions<JwtConfig> _Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="se"></param>
        /// <param name="con"></param>
        public LoginController(IService.Admin.IAdminService se, IOptions<JwtConfig> con)
        {
            _Config = con;
            _Service = se;
        }

        //[HttpGet]
        //public async Task<RM_ApiResult> Get()
        //{
        //    return await _Service.AdminLogin(new TAdminInfo());
        //}
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(Par_AdminLogin par)
        {
            var result = await _Service.AdminLogin(par);

            if (result.Status == Enum_SysAttributeType.Http.Success)
            {
                var data = result.Data as RM_AdminLogin;

                data.Token = AdminJwtHelper.GetTocken(data.User, _Config.Value);
                return result;
            }

            return result;
        }
    }
}