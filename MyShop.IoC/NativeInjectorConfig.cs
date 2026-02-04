using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Application;
using MyShop.Application.Identity;
using MyShop.Application.Identity.implement;
using MyShop.Application.Redis;
using MyShop.Application.Redis.implement;
using MyShop.Domain;
using MyShop.Infrastructure.EF;
using MyShop.Models.Dto.Account;
using MyShop.Shared.SysConfigs;
using StackExchange.Redis;

namespace MyShop.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterService(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();

            services.AddSingleton<IPasswordHasher<AccountDto>, PasswordHasher<AccountDto>>();

            services.AddSingleton<IConnectionMultiplexer>(_ =>
            {
                var options = ConfigurationOptions.Parse(ConfigManager.ConnectionStrings.Redis);

                options.AbortOnConnectFail = false;
                options.ConnectRetry = 5;
                options.ConnectTimeout = 10_000;
                options.SyncTimeout = 10_000;
                options.ReconnectRetryPolicy = new LinearRetry((int)TimeSpan.FromSeconds(5).TotalMilliseconds);

                return ConnectionMultiplexer.Connect(options);
            });
            services.AddSingleton<IRedisService, RedisService>();

            // 統一登入入口
            services.AddScoped<ILoginManagerService, LoginManagerService>();
            // 多實作介面：ILoginService（會注入到 IEnumerable<ILoginService>）
            services.AddScoped<ILoginService, AdminUserLoginService>();
            services.AddScoped<ILoginService, VendorUserLoginService>();
            services.AddScoped<ILoginService, MemberUserLoginService>();

            // 固定線
            services.AddScoped<IMyShopDbContextFactory, MyShopDbContextFactory>();
            // 重試策略
            services.AddScoped<IExecutionStrategyFactory, EfExecutionStrategyFactory>();

            // 慣例掃描
            var servicesAsm = typeof(BaseService).Assembly;
            var efAsm = typeof(MyShopDbContextFactory).Assembly;
            services.RegisterByConvention(servicesAsm, efAsm);
        }
    }
}
