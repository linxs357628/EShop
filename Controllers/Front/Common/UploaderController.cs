namespace EShop.Controllers.Front.Common
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/front/common/[controller]")]
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
        /// 前台 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<RM_ApiResult> Post(IFormFile file)
        {
            return await _Service.FrontUpLoad(file);
        }
    }
}