using Model.Parameter.Admin;

namespace EShop.Controllers.Admin.PeopleManage
{
    /// <summary>
    /// 管理人员列表
    /// </summary>
    [Authorize]
    [Route("api/admin/PeopleManage/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IService.Admin.IAdminService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public ListController(IService.Admin.IAdminService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 获取管理人员列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get(string keyword = "", int pageNo = 1, int pageSize = 20)
        {
            keyword = keyword?.Trim();
            return await _Service.GetPage(new Model.Parameter.Admin.Par_AdminPageBase { KeyWord = keyword, PageNo = pageNo, PageSize = pageSize });
        }

        /// <summary>
        /// 获取管理人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RM_ApiResult> Get(int id)
        {
            return await _Service.GetItem(id);
        }

        /// <summary>
        /// 新增管理人员/编辑管理人员
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(Par_AdminInfo_Power par)
        {
            return await _Service.ActionItem(par);
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