using IService.Front;

namespace EShop.Controllers.Front.Info
{
    [Route("api/front/info/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        private readonly ICommonService _Service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public CustomController(ICommonService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 获取用户协议
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_FrontApiReturn<dynamic>> Get()
        {
            return await _Service.GetCustom();
        }
    }
}