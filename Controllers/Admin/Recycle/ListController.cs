using IService.Admin;

namespace EShop.Controllers.Admin.Recycle
{
    /// <summary>
    /// 回收申请列表
    /// </summary>
    [Authorize]
    [Route("api/admin/Recycle/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IRecycleReqHandleService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="se"></param>
        public ListController(IRecycleReqHandleService se)
        {
            _Service = se;
        }

        /// <summary>
        /// 获取回收申请列表
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
        /// 获取回收申请详情
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
        public async Task<RM_ApiResult> Post(TRecycleReqHandle par)
        {
            return await _Service.AddOrUpdate(par);
        }

        /// <summary>
        /// 删除管理人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<RM_ApiResult> Delete(int id)
        {
            return await _Service.DeleteItem(id);
        }
    }
}