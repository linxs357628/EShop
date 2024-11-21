using Model.Ext.BaiDuMap;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace EShop.Domain
{
    /// <summary>
    ///
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 是否是ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //return request.Headers.ContainsKey("X-Requested-With") &&
            //       request.Headers["X-Requested-With"].Equals("XMLHttpRequest");

            return request.Headers["X-Requested-With"] == "XMLHttpRequest" || request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserIp(this HttpContext context)
        {
            if (context == null) return "";
            var result = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(result))
            {
                result = context.Connection.RemoteIpAddress?.ToString();
            }
            if (string.IsNullOrEmpty(result))
                throw new Exception("获取IP失败");

            if (result.Contains("::1"))
                result = "127.0.0.1";

            result = result.Replace("::ffff:", "");
            result = result.Split(':')?.FirstOrDefault() ?? "127.0.0.1";
            result = IsIP(result) ? result : "127.0.0.1";
            return result;
        }

        /// <summary>
        /// 判断是否IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获取登录用户id
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetUId(this HttpContext context)
        {
            int uid = 0;
            if (context.User?.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var claim = identity.FindFirst("AdminUserID")?.Value;
                if (claim != null)
                {
                    uid = int.Parse(claim);
                }
            }
            return uid;
        }

        /// <summary>
        /// 获取登录用户渠道id
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetChannelId(this HttpContext context)
        {
            int uid = 0;
            if (context.User?.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var claim = identity.FindFirst("ChannelId")?.Value;
                if (claim != null)
                {
                    uid = int.Parse(claim);
                }
            }
            return uid;
        }

        /// <summary>
        /// 获取登录用户类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetUserType(this HttpContext context)
        {
            int uid = 0;
            if (context.User?.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var claim = identity.FindFirst("UserType")?.Value;
                if (claim != null)
                {
                    uid = Convert.ToInt32(claim);
                }
            }
            return uid;
        }

        /// <summary>
        /// 获取登录用户名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetName(this HttpContext context)
        {
            string UserName = string.Empty;
            if (context.User?.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var claim = identity.FindFirst(ClaimTypes.Name)?.Value;
                if (claim != null)
                {
                    UserName = claim;
                }
            }
            return UserName;
        }

        /// <summary>
        /// 判断是否是管理员
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsAdmin(this HttpContext context)
        {
            var isAdmin = false;
            if (context.User?.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var claim = identity.FindFirst("Grade")?.Value;
                if (claim != null)
                {
                    isAdmin = claim.Equals("Administrator");
                }
            }
            return isAdmin;
        }

        /// <summary>
        /// ClaimsIdentity
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IEnumerable<ClaimsIdentity> GetClaims(this HttpContext context)
        {
            return context.User?.Identities;
        }

        //public static int GetRole(this HttpContext context)
        //{
        //    var roleid = context.User.FindFirstValue(ClaimTypes.Role) ?? "0";

        //    return int.Parse(roleid);
        //}

        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpContext context)
        {
            return context.Request.Headers["User-Agent"];
        }

        /// <summary>
        /// 获取请求令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetToken(this HttpContext context)
        {
            return context.Request.Headers["Authorization"];
        }

        /// <summary>
        /// 获取请求Url
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestUrl(this HttpContext context)
        {
            return context != null ? context.Request.Path.Value : "";
        }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetQueryString(this HttpContext context)
        {
            return context != null ? context.Request.QueryString.Value : "";
        }

        /// <summary>
        /// 获取body请求参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetBody(this HttpContext context)
        {
            context.Request.EnableBuffering();
            //context.Request.Body.Seek(0, SeekOrigin.Begin);
            //using var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
            ////需要使用异步方式才能获取
            //return reader.ReadToEndAsync().Result;
            string body = string.Empty;
            var buffer = new MemoryStream();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            context.Request.Body.CopyToAsync(buffer);
            buffer.Position = 0;
            try
            {
                using StreamReader streamReader = new(buffer, Encoding.UTF8);
                body = streamReader.ReadToEndAsync().Result;
            }
            finally
            {
                buffer?.Dispose();
            }
            return body;
        }

        /// <summary>
        /// 根据IP获取地理位置
        /// </summary>
        /// <returns></returns>
        public static async Task<IpData> GetIpInfo(string IP)
        {
            IpData ipinfo = new();
            if (IP == "127.0.0.1")
            {
                ipinfo.country = "内网IP";
            }
            else
            {
                try
                {
                    using HttpClient httpClient = new();
                    //设置设备
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");
                    //取到服务地址请求
                    var response = await httpClient.GetAsync("https://qifu-api.baidubce.com/ip/geo/v1/district?ip=" + IP);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        IPExt ipext = JsonConvert.DeserializeObject<IPExt>(responseBody);
                        if (ipext.msg.Contains("成功"))
                        {
                            ipinfo = ipext.data;
                        }
                        else
                        {
                            ipinfo.country = "未知IP";
                        }
                    }
                    else
                    {
                        ipinfo.country = "未知IP";
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.ToString());
                    ipinfo.country = "IP解析错误";
                }
            }
            return ipinfo;
        }

        /// <summary>
        /// 设置请求参数
        /// </summary>
        /// <param name="reqMethod"></param>
        /// <param name="context"></param>
        public static string GetRequestValue(this HttpContext context, string reqMethod)
        {
            string param = string.Empty;

            if (HttpMethods.IsPost(reqMethod) || HttpMethods.IsPut(reqMethod) || HttpMethods.IsDelete(reqMethod))
            {
                param = context.GetBody();
                param = param.Replace("\n", "").Replace(" ", "");
                string regex = "(?<=\"password\":\")[^\",]*";
                param = Regex.Replace(param, regex, "***");
            }
            if (string.IsNullOrEmpty(param))
            {
                param = context.GetQueryString();
            }
            return param;
        }
    }
}