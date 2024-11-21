using Model.Parameter.Admin;

namespace EShop.Controllers.Admin.Common
{
    /// <summary>
    /// 修改密码
    /// </summary>
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ChangepasswordController : ControllerBase
    {
        private readonly IService.Admin.IAdminService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="se"></param>
        public ChangepasswordController(IService.Admin.IAdminService se)
        {
            _Service = se;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(Par_AdminChangePwd par)
        {
            return await _Service.ChangePwd(par);
        }
    }
}