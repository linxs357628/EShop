using IService.Front;
using Model.Parameter.Admin;
using Model.Result.Front;

namespace EShop.Controllers.Front.My
{
    /// <summary>
    /// 会员信息
    /// </summary>

    [Authorize]
    [Route("api/front/my/[controller]")]
    [ApiController]
    public class BaseInfoController : ControllerBase
    {
        private readonly ICustomerService _Service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public BaseInfoController(ICustomerService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 会员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<RM_FrontApiReturn<RM_Customerinfo>> Get()
        {
            return _Service.BaseInfo();
        }

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_FrontApiReturn<object>> Post(Par_Customerinfo par)
        {
            return (await _Service.UpdateCustomer(par)).Adapt<RM_FrontApiReturn<object>>();
        }
    }
}