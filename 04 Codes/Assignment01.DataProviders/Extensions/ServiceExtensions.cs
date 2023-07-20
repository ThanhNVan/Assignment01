using Microsoft.Extensions.DependencyInjection;

namespace Assignment01.DataProviders;

public static class ServiceExtensions
{
    public static void AddDataProviders(this IServiceCollection services) {
        services.AddScoped<ICategoryDataProviders, CategoryDataProviders>();
        services.AddScoped<IMemberDataProviders, MemberDataProviders>();
        services.AddScoped<IOrderDataProviders, OrderDataProviders>();
        services.AddScoped<IOrderDetailDataProviders, OrderDetailDataProviders>();
        services.AddScoped<IProductDataProviders, ProductDataProviders>();
    }
}
