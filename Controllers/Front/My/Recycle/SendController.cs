using IService.Front;
using Model.Parameter.Front;

namespace EShop.Controllers.Front.My.Recycle
{
    /// <summary>
    /// 会员中心 -寄送回收
    /// </summary>

    [Authorize]
    [Route("api/front/my/recycle/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly IRecycleSendService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SendController(IRecycleSendService service)
        {
            _service = service;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<RM_FrontApiReturn<object>> Post(Par_RecycleSend par)
        {
            var req = par.Adapt<TRecycleSend>();
            return (await _service.AddOrUpdate(req)).Adapt<RM_FrontApiReturn<object>>();
        }
    }
}