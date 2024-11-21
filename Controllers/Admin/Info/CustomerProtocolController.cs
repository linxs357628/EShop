using IService.Admin;
using Model.Parameter.Admin;

namespace EShop.Controllers.Admin.Info
{
    /// <summary>
    /// 客户协议
    /// </summary>
    [Authorize]
    [Route("api/admin/info/[controller]")]
    [ApiController]
    public class CustomerProtocolController : ControllerBase
    {
        private readonly ICustomerProtocolService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="se"></param>
        public CustomerProtocolController(ICustomerProtocolService se)
        {
            _Service = se;
        }

        /// <summary>
        /// 获取客户协议
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get()
        {
            return await _Service.GetFirst();
        }

        /// <summary>
        /// 更新客户协议
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(Par_CustomerProtocol par)
        {
            var o = par.Adapt<TCustomerProtocol>();
            o.Id = 1;
            return await _Service.AddOrUpdate(o);
        }
    }
}