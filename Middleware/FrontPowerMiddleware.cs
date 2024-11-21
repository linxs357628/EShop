using System.Text.RegularExpressions;

namespace EShop.Middleware
{
    /// <summary>
    ///
    /// </summary>
    public class FrontPowerMiddleware
    {
        private readonly IAuthSupportService _Service;
        private readonly RequestDelegate _next;
        private static readonly Regex _regex = new Regex($"/api/front/(.*)", RegexOptions.Compiled);
        private static readonly string _emptyJson = "{}";

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        /// <param name="se"></param>
        public FrontPowerMiddleware(RequestDelegate next, IAuthSupportService se)
        {
            _Service = se;
            _next = next;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var isNeed = _regex.Match(context.Request.Path);
            if (isNeed.Success)
            {
                var actionName = isNeed.Groups[1].Value.ToLower();
                var index = actionName.IndexOf('/');
                if (index != -1)
                {
                    actionName = actionName.Substring(0, index);
                }

                if (string.Equals(actionName, "my", StringComparison.OrdinalIgnoreCase))
                {
                    if (_Service.CheckToken4CustomerUser())
                    {
                        if (_Service.CheckBind4CustomerUser())
                        {
                            await _next(context);
                        }
                        else
                        {
                            await WriteResponse(context, 402, _emptyJson);
                        }
                    }
                    else
                    {
                        await WriteResponse(context, 401, _emptyJson);
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private static async Task WriteResponse(HttpContext context, int statusCode, string content)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(content);
        }
    }
}