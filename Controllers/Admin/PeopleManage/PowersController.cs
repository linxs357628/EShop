using IService.Admin;

namespace EShop.Controllers.Admin.PeopleManage
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [Route("api/admin/PeopleManage/[controller]")]
    [ApiController]
    public class PowersController : ControllerBase
    {
        private readonly IAdminPermissionService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public PowersController(IAdminPermissionService service)
        {
            _Service = service;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(PM_Powers par)
        {
            return await _Service.ActionPowers(par.Powers, par.AdminId);
        }

        /// <summary>
        ///
        /// </summary>
        public class PM_Powers
        {
            /// <summary>
            ///
            /// </summary>
            public List<int> Powers { get; set; }

            /// <summary>
            ///
            /// </summary>
            public int AdminId { get; set; }
        }
    }
}