using IService.Admin;

namespace EShop.Controllers.Admin.Common
{
    /// <summary>
    /// 权限
    /// </summary>
    [Authorize]
    [Route("api/admin[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IAdminService _Service;

        ///
        public InfoController(IAdminService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get()
        {
            return await _Service.CurrentAdminInfo();
        }
    }
}