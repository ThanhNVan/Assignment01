using Assignment01.DataProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment01.LogicProviders;

public static class ServiceExtensions
{
    public static void AddLogicProviders(this IServiceCollection services) {
        services.AddScoped<ICategoryLogicProviders, CategoryLogicProviders>();
        services.AddScoped<IMemberLogicProviders, MemberLogicProviders>();
        services.AddScoped<IOrderLogicProviders, OrderLogicProviders>();
        services.AddScoped<IOrderDetailLogicProviders, OrderDetailLogicProviders>();
        services.AddScoped<IProductLogicProviders, ProductLogicProviders>();

        services.AddDataProviders();
    }
}
