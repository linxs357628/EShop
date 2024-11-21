namespace EShop.Domain
{
    /// <summary>
    /// 自定义操作日志记录注解
    /// </summary>
    public class LogAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否保存请求数据
        /// </summary>
        public bool IsSaveRequestData { get; set; } = true;

        /// <summary>
        /// 是否保存返回数据
        /// </summary>
        public bool IsSaveResponseData { get; set; } = false;

        /// <summary>
        /// 是否保存IP地理位置
        /// </summary>
        public bool IsSaveIPLocation { get; set; } = true;

        /// <summary>
        ///
        /// </summary>
        public LogAttribute()
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">模块名称</param>
        /// <param name="saveRequestData">是否保存请求参数</param>
        /// <param name="saveResponseData">是否保存响应结果</param>
        /// <param name="saveIPLocation">是否保存Ip</param>
        public LogAttribute(string name, bool saveRequestData = true, bool saveResponseData = true, bool saveIPLocation = true)
        {
            Title = name;
            IsSaveRequestData = saveRequestData;
            IsSaveResponseData = saveResponseData;
            IsSaveIPLocation = saveIPLocation;
        }
    }
}