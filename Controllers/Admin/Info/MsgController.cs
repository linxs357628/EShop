using IService.Admin;
using Model.Parameter.Admin;

namespace EShop.Controllers.Admin.Info
{
    /// <summary>
    /// 留言列表
    /// </summary>
    [Authorize]
    [Route("api/admin/info/[controller]")]
    [ApiController]
    public class MsgController : ControllerBase
    {
        private readonly IMsgCtrlHandleService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="se"></param>
        public MsgController(IMsgCtrlHandleService se)
        {
            _Service = se;
        }

        /// <summary>
        /// 获取留言列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="searchKey"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get(string keyword = "", int searchKey = 0, int pageNo = 1, int pageSize = 20)
        {
            keyword = keyword?.Trim();
            return await _Service.GetPage(new Model.Parameter.Admin.Par_AdminPageBase { KeyWord = keyword, SearchKey = searchKey, PageNo = pageNo, PageSize = pageSize });
        }

        /// <summary>
        /// 获取留言详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RM_ApiResult> Get(int id)
        {
            return await _Service.GetItem(id);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(Par_MsgCtrlHandle par)
        {
            var req = par.Adapt<TMsgCtrlHandle>();
            req.MsgId = par.Id;
            req.Id = 0;

            return await _Service.AddOrUpdate(req);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<RM_ApiResult> Delete(int id)
        {
            return await _Service.Deleted(id);
        }
    }
}