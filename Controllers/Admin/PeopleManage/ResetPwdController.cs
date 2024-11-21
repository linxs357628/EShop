using IService.Admin;

namespace EShop.Controllers.Admin.PeopleManage
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [Route("api/admin/PeopleManage/[controller]")]
    [ApiController]
    public class ResetPwdController : ControllerBase
    {
        private readonly IAdminService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public ResetPwdController(IAdminService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<RM_ApiResult> Put(int id)
        {
            return await _Service.ResetPassWord(id);
        }
    }
}