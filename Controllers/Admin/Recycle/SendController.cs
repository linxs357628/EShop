using IService.Admin;

namespace EShop.Controllers.Admin.Recycle
{
    /// <summary>
    /// 寄送处理
    /// </summary>
    [Authorize]
    [Route("api/admin/Recycle/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly IRecycleSendHandleService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="se"></param>
        public SendController(IRecycleSendHandleService se)
        {
            _Service = se;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(TRecycleSendHandle par)
        {
            return await _Service.AddOrUpdate(par);
        }
    }
}