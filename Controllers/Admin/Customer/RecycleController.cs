using IService.Admin;
using Model.Parameter.Admin;

namespace EShop.Controllers.Admin.Customer
{
    /// <summary>
    /// 会员回收列表
    /// </summary>
    [Authorize]
    [Route("api/admin/customer/[controller]")]
    [ApiController]
    public class RecycleController : ControllerBase
    {
        private readonly ICustomerService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="se"></param>
        public RecycleController(ICustomerService se)
        {
            _Service = se;
        }

        /// <summary>
        /// 获取会员回收列表
        /// </summary>
        /// <param name="customerKey"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get(int customerKey, int pageNo = 1, int pageSize = 20)
        {
            return await _Service.GetRecycleListByCustomerKey(new Par_CustomerAsset { PageNo = pageNo, PageSize = pageSize, CustomerKey = customerKey, });
        }
    }
}