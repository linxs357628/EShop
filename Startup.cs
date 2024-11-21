using Autofac;
using EShop.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace disiduw
{
    /// <summary>
    ///
    /// </summary>
    public class Startup
    {
        private readonly string _MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();
            services.AddSingleton(new AppSettings(Configuration));
            services.AddMemoryCache();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeConverter());
                options.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeNullableConverter());
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });

            #region jwt验证

            var token = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                //Token Validation Parameters
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证。
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    //获取或设置一个System.String，它表示将使用的有效发行者检查代币的发行者。
                    ValidIssuer = token.Issuer,
                    //获取或设置一个字符串，该字符串表示将用于检查的有效受众反对令牌的观众。
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateActor = false
                };
            });

            #endregion jwt验证

            services.AddCors(options =>
            {
                options.AddPolicy(_MyAllowSpecificOrigins,

                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()

                );
            });

            //services.AddMultiTenancy()
            //    .WithResolutionStrategy<HostResolutionStrategy>()
            //    .WithStore<InMemoryTenantStore>();

            services.AddSqlsugarSetup(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "后端接口", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "前端接口", Version = "v2" });

                var xmlFile = $"EShop.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var modelFile = $"Model.xml";
                var modelPath = Path.Combine(AppContext.BaseDirectory, modelFile);
                c.IncludeXmlComments(modelPath);

                #region 设置文档添加验证

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion 设置文档添加验证

                c.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                        return false;

                    var routeTemplate = "/" + methodInfo.DeclaringType.GetCustomAttributes(true)
                                   .OfType<RouteAttribute>()
                                   .Select(attr => attr.Template.TrimStart('/'))
                                   .FirstOrDefault();

                    if (routeTemplate.StartsWith("/api/admin"))
                        return version == "v1";
                    else if (routeTemplate.StartsWith("/api/front"))
                        return version == "v2";
                    else
                        return false;
                });
            });

            //HangFire
            //services.AddHangfire(x =>
            //{
            //    x.UseSqlServerStorage(Configuration.GetConnectionString("hangFire"),
            //        new SqlServerStorageOptions()
            //        {
            //            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //            QueuePollInterval = TimeSpan.Zero,
            //            UseRecommendedIsolationLevel = true,
            //            UsePageLocksOnDequeue = true,
            //            DisableGlobalLocks = true
            //        }
            //    );
            //});
            //services.AddHangfireServer();

            //后台配置注入
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            //services.Configure<List<Tenant>>(Configuration.GetSection("Tenants"));
            services.Configure<List<MutiDBOperate>>(Configuration.GetSection("DBS"));
            services.Configure<RedisCon>(Configuration.GetSection("RedisCon"));
            services.Configure<WebMiscConfig>(Configuration.GetSection("WebMiscConfig"));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_Builder"></param>
        public void ConfigureContainer(ContainerBuilder _Builder)
        {
            var iService = Assembly.Load("IService");
            var rService = Assembly.Load("Service");

            _Builder.RegisterAssemblyTypes(iService, rService).Where(i => i.Name.EndsWith("Service")).AsImplementedInterfaces();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "后端接口");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "前端接口");
                c.EnablePersistAuthorization();
            });

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            string dir = AppSettings.GetConfig("PhysicalPath");//指定文件读取物理路径
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(dir),
                RequestPath = "/file"
            });

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    ServeUnknownFileTypes = true,
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            //});

            app.UseCors(_MyAllowSpecificOrigins);

            app.UseRouting();

            //日志
            //app.UseOperLogMiddleware();

            #region Hangfire

            //HangFire
            //var filter = new BasicAuthAuthorizationFilter(
            //    new BasicAuthAuthorizationFilterOptions
            //    {
            //        SslRedirect = false,
            //        // Require secure connection for dashboard
            //        RequireSsl = false,
            //        // Case sensitive login checking
            //        LoginCaseSensitive = false,
            //        // Users
            //        Users = new[]
            //        {
            //            new BasicAuthAuthorizationUser
            //            {
            //                Login = "hangfire",
            //                PasswordClear = "hangfire"
            //            }
            //        }
            //    });
            //var options = new DashboardOptions
            //{
            //    AppPath = "/",//返回时跳转的地址
            //    DisplayStorageConnectionString = false,//是否显示数据库连接信息
            //    Authorization = new[] {
            //        filter
            //    },
            //    IsReadOnlyFunc = Context =>
            //    {
            //        return false;//是否只读面板
            //    }
            //};
            //app.UseHangfireDashboard("/job", options);
            //HangfireDispose.HangfireService();

            #endregion Hangfire

            //app.UseMiddleware<DebounceMiddleware>();
            //多租户
            //app.UseMultiTenancy();

            app.UseAuthentication();
            app.UseAuthorization();

            //权限判断，需在jwt验证后
            app.UseMiddleware<PowerMiddleware>();
            //用户信息
            app.UseMiddleware<FrontPowerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //日志
                //app.UseMiddleware<OperLogMiddleware>();
            });
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class SqlsugarSetup
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            List<MutiDBOperate> listdatabase = new();

            string Path = "appsettings.json";
            using (var file = new StreamReader(Path))
            using (var reader = new JsonTextReader(file))
            {
                var jObj = (JObject)JToken.ReadFrom(reader);
                if (!string.IsNullOrWhiteSpace("DBS"))
                {
                    var secJt = jObj["DBS"];
                    if (secJt != null)
                    {
                        for (int i = 0; i < secJt.Count(); i++)
                        {
                            if (bool.Parse(secJt[i]["Enabled"].ToString()))
                            {
                                listdatabase.Add(new MutiDBOperate()
                                {
                                    ConnId = secJt[i]["ConnId"].ToString(),
                                    Conn = secJt[i]["Connection"].ToString(),
                                    HitRate = Convert.ToInt32(secJt[i]["HitRate"].ToString()),
                                });
                            }
                        }
                    }
                }
            }

            var listConfig = new List<ConnectionConfig>();
            var expMethods = new List<SqlFuncExternal>();
            ICacheService myCache = new SugarCache();
            listdatabase.ForEach(m =>
                {
                    listConfig.Add(new ConnectionConfig()
                    {
                        ConfigId = m.ConnId,
                        ConnectionString = m.Conn,
                        DbType = DbType.MySql,
                        IsAutoCloseConnection = true,
                        //IsShardSameThread = false, //用SqlSugarScope单例模式取代SqlSugarClient+IsShardSameThread效果一样，并且支持异步，
                        AopEvents = new AopEvents
                        {
                            OnLogExecuting = (sql, p) =>
                            {
                            }
                        },
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true
                        },
                        ConfigureExternalServices = new ConfigureExternalServices()
                        {
                            SqlFuncServices = expMethods,//set ext method,
                            DataInfoCacheService = myCache
                        }
                    }
                   );
                });

            var sugarClient = new SqlSugarScope(listConfig);
            StaticConfig.EnableAllWhereIF = true;
            services.AddSingleton<ISqlSugarClient>(sugarClient);//这边是SqlSugarScope用AddSingleton
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SystemTextJsonConvert
    {
        /// <summary>
        ///
        /// </summary>
        public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="typeToConvert"></param>
            /// <param name="options"></param>
            /// <returns></returns>
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.Parse(reader.GetString());
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="value"></param>
            /// <param name="options"></param>
            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class DateTimeNullableConverter : System.Text.Json.Serialization.JsonConverter<DateTime?>
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="typeToConvert"></param>
            /// <param name="options"></param>
            /// <returns></returns>
            public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : DateTime.Parse(reader.GetString());
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="value"></param>
            /// <param name="options"></param>
            public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
}