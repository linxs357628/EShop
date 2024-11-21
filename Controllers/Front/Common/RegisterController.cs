using IService.Front;
using Model.Parameter.Front;

namespace EShop.Controllers.Front.Common
{
    /// <summary>
    /// 用户注册
    /// </summary>

    [Route("api/front/common/form/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ICustomerService _Service;

        public RegisterController(ICustomerService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 获取默认头像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RM_FrontApiReturn<object> Get()
        {
            return _Service.GetDefaultAvator().Adapt<RM_FrontApiReturn<object>>();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_FrontApiReturn<object>> Post(Par_Customerinfo1 par)
        {
            return (await _Service.RegisterCustomer(par)).Adapt<RM_FrontApiReturn<object>>();
        }
    }
}