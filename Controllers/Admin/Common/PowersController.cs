using IService.Admin;

namespace EShop.Controllers.Admin.Common
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [Route("api/admin[controller]")]
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RM_ApiResult> Get(int id)
        {
            return await _Service.GetAllPowers(id);
        }
    }
}