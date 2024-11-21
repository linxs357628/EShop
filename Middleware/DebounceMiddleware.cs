using System.Collections.Concurrent;

namespace EShop.Middleware
{
    /// <summary>
    /// 接口防抖
    /// </summary>
    public class DebounceMiddleware
    {
        private readonly RequestDelegate _next;
        private static ConcurrentDictionary<string, DateTime> _lastApiCalls = new ConcurrentDictionary<string, DateTime>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        public DebounceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.Request.Path.Value;
            if (context.Request.Method == HttpMethods.Post && IsDebounced(endpoint))
            {
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsync("Too many requests, please try again later.");
                return;
            }

            await _next(context);
        }

        private bool IsDebounced(string endpoint)
        {
            var now = DateTime.UtcNow;
            var lastCall = _lastApiCalls.GetOrAdd(endpoint, now);
            if (now - lastCall < TimeSpan.FromSeconds(1))
            {
                return true;
            }

            _lastApiCalls[endpoint] = now;
            return false;
        }
    }
}