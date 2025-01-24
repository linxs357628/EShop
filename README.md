这个一个基于C#开发的Web API项目，从文件结构和代码内容来看，它可能是一个电商相关的应用程序，包含了前后台的一些管理功能。以下是对该仓库主要部分的介绍：

### 项目结构
- **根目录文件**
    - `.gitattributes` 和 `.gitignore`：用于Git版本控制，指定文件属性和忽略规则。
    - `EShop.csproj`：C#项目文件，包含项目的配置信息，如引用的库、编译选项等。
    - `EShop.http`：可能是用于HTTP请求测试的文件。
    - `EShop.sln`：Visual Studio解决方案文件，管理项目和其依赖项。
    - `GlobalUsings.cs`：全局使用的命名空间声明文件，可减少代码中重复的`using`语句。
    - `Program.cs` 和 `Startup.cs`：ASP.NET Core应用程序的入口点和启动配置文件。
    - `appsettings.Development.json` 和 `appsettings.json`：应用程序的配置文件，包含不同环境下的设置，如日志级别等。
    - `launchSettings.json`：用于配置应用程序的启动设置，如端口号、环境变量等。
- **文件夹**
    - `Domain`：可能包含领域模型和业务逻辑相关的代码。
        - `HttpContextExtensions.cs`：可能是对`HttpContext`的扩展方法。
        - `LogAttribute.cs`：可能是一个自定义的日志记录特性。
    - `Controllers`：包含前后台的控制器，负责处理HTTP请求。
        - `Admin`：后台管理相关的控制器，如用户管理、消息管理、回收申请管理等。
        - `Front`：前台用户相关的控制器，如会员信息管理、留言列表管理、回收列表管理等。
    - `JwtHelper`：与JSON Web Token（JWT）相关的帮助类。
        - `AdminJwtHelper.cs`：可能是用于生成和验证管理员JWT的帮助类。
        - `CustomerJwtHelper.cs`：可能是用于生成和验证用户JWT的帮助类。
    - `Middleware`：自定义中间件，用于处理HTTP请求的中间环节。
        - `DebounceMiddleware.cs`：可能是用于防止重复提交的中间件。
        - `FrontPowerMiddleware.cs` 和 `PowerMiddleware.cs`：可能是用于权限验证的中间件。

### 主要功能模块
- **后台管理**
    - **用户管理**：包括获取用户信息、获取用户列表、新增/编辑用户、删除用户等功能。
    - **消息管理**：消息列表的获取、全部已读、查看消息详情、删除消息等功能。
    - **回收申请管理**：回收申请列表的管理。
- **前台用户**
    - **会员信息管理**：获取会员信息、修改会员信息。
    - **留言列表管理**：获取留言列表、获取留言详情、新增留言、删除留言。
    - **回收列表管理**：获取回收列表、获取回收详情、新增回收、删除回收。
    - **登录**：用户账号登录。

### 技术栈
- **语言**：C#
- **框架**：ASP.NET Core，用于构建Web API应用程序。
- **身份验证**：使用JWT进行用户身份验证。

总体而言，这个仓库是一个功能较为丰富的电商管理系统的后端代码，包含了前后台的多个功能模块，使用了ASP.NET Core和JWT等技术。
