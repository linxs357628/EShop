namespace EShop.Controllers.Admin.Common
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/admin[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        private readonly IUploadService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public UploaderController(IUploadService service)
        {
            _Service = service;
        }

        /// <summary>
        /// 后台 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(IFormFile file)
        {
            return await _Service.AdminUpLoad(file);
        }
    }
}