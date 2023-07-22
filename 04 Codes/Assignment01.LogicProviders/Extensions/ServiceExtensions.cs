using Assignment01.DataProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment01.LogicProviders;

public static class ServiceExtensions
{
    public static void AddLogicProviders(this IServiceCollection services) {
        services.AddTransient<ICategoryLogicProviders, CategoryLogicProviders>();
        services.AddTransient<IMemberLogicProviders, MemberLogicProviders>();
        services.AddTransient<IOrderLogicProviders, OrderLogicProviders>();
        services.AddTransient<IOrderDetailLogicProviders, OrderDetailLogicProviders>();
        services.AddTransient<IProductLogicProviders, ProductLogicProviders>();

        services.AddTransient<LogicContext>();

        services.AddDataProviders();
    }
}
