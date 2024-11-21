using IService.Front;

namespace EShop.Controllers.Front.My
{
    /// <summary>
    /// 退出登录
    /// </summary>

    [Authorize]
    [Route("api/front/my/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly ICustomerService _Service;

        public LogoutController(ICustomerService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_FrontApiReturn<object>> Get()
        {
            return await _Service.Logout();
        }
    }
}