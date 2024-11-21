using IService.Front;

namespace EShop.Controllers.Front.Common
{
    /// <summary>
    /// 下拉框接口
    /// </summary>
    [Route("api/front/common/[controller]/[action]")]
    [ApiController]
    public class SelectController : ControllerBase
    {
        private readonly ICustomerService _Service;
        private readonly JwtConfig _Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="se"></param>
        /// <param name="con"></param>
        public SelectController(ICustomerService se, IOptions<JwtConfig> con)
        {
            _Service = se;
            _Config = con.Value;
        }

        /// <summary>
        /// 获取性别下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RM_FrontApiReturn<object> Sex()
        {
            return _Service.GetSexSelect().Adapt<RM_FrontApiReturn<object>>();
        }

        /// <summary>
        /// 获取来源下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RM_FrontApiReturn<object> Source()
        {
            return _Service.GetSourceSelect().Adapt<RM_FrontApiReturn<object>>();
        }
    }
}