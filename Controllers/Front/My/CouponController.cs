using IService.Front;

namespace EShop.Controllers.Front.My
{
    /// <summary>
    /// 会员券包
    /// </summary>

    [Authorize]
    [Route("api/front/my/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICustomerService _Service;
        private readonly IService.Admin.ICustomerService _customerService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public CouponController(ICustomerService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 会员券包
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_FrontApiReturn<object>> Get()
        {
            return (await _Service.GetCouponList()).Adapt<RM_FrontApiReturn<object>>();
        }
    }
}