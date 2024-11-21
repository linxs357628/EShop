using IService.Front;
using Model.Parameter.Front;

namespace EShop.Controllers.Front.My.Recycle
{
    /// <summary>
    /// 会员中心 -回收列表
    /// </summary>

    [Authorize]
    [Route("api/front/my/recycle/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IRecycleReqService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public ListController(IRecycleReqService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取回收列表
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_FrontApiReturn<RM_PageReturn<TRecycleReq>>> Get(int pageNo, int pageSize)
        {
            return (await _service.GetPageByCurrent(pageNo, pageSize)).Adapt<RM_FrontApiReturn<RM_PageReturn<TRecycleReq>>>();
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RM_FrontApiReturn<TRecycleReq>> Get(int id)
        {
            return (await _service.GetItem(id)).Adapt<RM_FrontApiReturn<TRecycleReq>>();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<RM_FrontApiReturn<object>> Post(Par_RecycleReq par)
        {
            var req = par.Adapt<TRecycleReq>();
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