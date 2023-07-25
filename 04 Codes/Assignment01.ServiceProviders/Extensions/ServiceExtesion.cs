using Microsoft.Extensions.DependencyInjection;

namespace Assignment01.ServiceProviders;

public static class ServiceExtesion
{
    public static void AddServices(this IServiceCollection services) {
        services.AddTransient<ICategoryServiceProviders, CategoryServiceProviders>();
        services.AddTransient<IMemberServiceProviders, MemberServiceProviders>();
        services.AddTransient<IOrderServiceProviders, OrderServiceProviders>();
        services.AddTransient<IOrderDetailServiceProviders, OrderDetailServiceProviders>();
        services.AddTransient<IProductServiceProviders, ProductServiceProviders>();

        services.AddTransient<ServiceContext>();
    }
}
