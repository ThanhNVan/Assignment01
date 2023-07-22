using Microsoft.Extensions.DependencyInjection;

namespace Assignment01.DataProviders;

public static class ServiceExtensions
{
    public static void AddDataProviders(this IServiceCollection services) {
        services.AddTransient<ICategoryDataProviders, CategoryDataProviders>();
        services.AddTransient<IMemberDataProviders, MemberDataProviders>();
        services.AddTransient<IOrderDataProviders, OrderDataProviders>();
        services.AddTransient<IOrderDetailDataProviders, OrderDetailDataProviders>();
        services.AddTransient<IProductDataProviders, ProductDataProviders>();

        services.AddTransient<DataContext>();
    }
}
