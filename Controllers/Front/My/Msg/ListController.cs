using IService.Front;
using Model.Parameter.Front;

namespace EShop.Controllers.Front.My.Msg
{
    /// <summary>
    /// 会员中心 -留言列表
    /// </summary>

    [Authorize]
    [Route("api/front/my/msg/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IMsgCtrlService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public ListController(IMsgCtrlService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取留言列表
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_FrontApiReturn<RM_PageReturn<TMsgCtrl>>> Get(int pageNo, int pageSize)
        {
            return (await _service.GetPageByCurrent(pageNo, pageSize)).Adapt<RM_FrontApiReturn<RM_PageReturn<TMsgCtrl>>>();
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RM_FrontApiReturn<TMsgCtrl>> Get(int id)
        {
            return (await _service.GetItem(id)).Adapt<RM_FrontApiReturn<TMsgCtrl>>();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<RM_FrontApiReturn<object>> Post(Par_MsgCtrlMenber par)
        {
            var req = par.Adapt<TMsgCtrl>();
            req.Source = Model.Enums.Common.Enum_Source.member;

            return (await _service.AddOrUpdate(req)).Adapt<RM_FrontApiReturn<object>>();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<RM_FrontApiReturn<object>> Delete(int id)
        {
            return (await _service.Delete(id)).Adapt<RM_FrontApiReturn<object>>();
        }
    }
}