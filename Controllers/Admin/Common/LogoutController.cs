namespace EShop.Controllers.Admin.Common
{
    /// <summary>
    /// 退出登录
    /// </summary>
    [Authorize]
    [Route("api/admin[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IService.Admin.IAdminService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public LogoutController(IService.Admin.IAdminService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get()
        {
            return await _Service.AdminLogout();
        }
    }
}