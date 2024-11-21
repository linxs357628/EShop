using EShop.JwtHelper;
using IService.Front;
using Model.Parameter.Front;
using Model.Result.Front;

namespace EShop.Controllers.Front.Common
{
    /// <summary>
    /// 用户账号登录
    /// </summary>
    [Route("api/front/common/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ICustomerService _Service;
        private readonly JwtConfig _Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="se"></param>
        /// <param name="con"></param>
        public LoginController(ICustomerService se, IOptions<JwtConfig> con)
        {
            _Service = se;
            _Config = con.Value;
        }

        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="par">登录模型</param>
        /// <returns></returns>

        [HttpPost]
        public async Task<RM_FrontApiReturn<RM_Customer>> Post(Par_CustomerLogin par)
        {
            var result = await _Service.LoginByAccount(par.Account, par.PassWord);

            if (result.Status == Enum_SysAttributeType.Http.Success)
            {
                CustomerJwtHelper.GetTocken(result.Data as RM_Customer, _Config);
            }

            return result.Adapt<RM_FrontApiReturn<RM_Customer>>();
        }
    }
}