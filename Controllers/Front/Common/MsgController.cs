using IService.Front;
using Model.Parameter.Front;

namespace EShop.Controllers.Front.Common
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/front/common/[controller]")]
    [ApiController]
    public class MsgController : ControllerBase
    {
        private readonly IMsgCtrlService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public MsgController(IMsgCtrlService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 游客发送留言
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_FrontApiReturn<object>> Post(Par_MsgCtrl par)
        {
            var req = par.Adapt<TMsgCtrl>();
            req.Source = Model.Enums.Common.Enum_Source.tourists;
            return (await _Service.AddOrUpdate(req)).Adapt<RM_FrontApiReturn<object>>();
        }
    }
}