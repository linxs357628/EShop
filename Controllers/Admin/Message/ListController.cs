using IService.Admin;

namespace EShop.Controllers.Admin.Message
{
    /// <summary>
    /// 提醒中心
    /// </summary>
    [Authorize]
    [Route("api/admin/notice/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IMessageService _Service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="se"></param>
        public ListController(IMessageService se)
        {
            _Service = se;
        }

        /// <summary>
        /// 消息列表
        /// </summary>
        /// <param name="cateId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get(int cateId = 0, int pageNo = 1, int pageSize = 20)
        {
            return await _Service.GetPage4CurrentAdmin(new Model.Parameter.Admin.Par_AdminPageBase { CategoryId = cateId, PageNo = pageNo, PageSize = pageSize });
        }

        /// <summary>
        /// 全部已读
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post()
        {
            return await _Service.AllRead4CurrentAdmin();
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RM_ApiResult> Get(int id)
        {
            return await _Service.GetItem(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<ListController>/5
        [HttpDelete("{id}")]
        public async Task<RM_ApiResult> Delete(int id)
        {
            return await _Service.DeleteMessage4CurrentAdmin(new System.Collections.Generic.List<int> { id });
        }
    }
}