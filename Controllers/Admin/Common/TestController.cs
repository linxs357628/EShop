using System.Reflection;

namespace EShop.Controllers.Admin.Common
{
    /// <summary>
    /// 创建表
    /// </summary>
    [Route("api/admin[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IService.Admin.IAdminService _Service;

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public TestController(IService.Admin.IAdminService service)
        {
            _Service = service;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task Get()
        {
            var list = Assembly.Load("Model").GetTypes().Where(u => u.IsClass && u.Namespace == "Model.Entites").ToArray();
            _Service.CreateTable(types: list);

            _Service.SetI18n();
        }
    }
}