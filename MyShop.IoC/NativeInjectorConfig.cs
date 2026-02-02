using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Application;
using MyShop.Domain;
using MyShop.Infrastructure.EF;

namespace MyShop.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterService(this IServiceCollection services, IConfiguration config)
        {
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
