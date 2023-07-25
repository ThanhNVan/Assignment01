namespace Assignment01.ServiceProviders;

public class ServiceContext
{
	#region [ CTor ]
	public ServiceContext(ICategoryServiceProviders categories,
							IOrderServiceProviders orders,
							IOrderDetailServiceProviders ordersDetails,
                            IMemberServiceProviders member,
							IProductServiceProviders products) {
        this.Categories = categories;
        this.Orders = orders;
        this.OrdersDetails = ordersDetails;
        this.Members = member;
		this.Products = products;
    }
	#endregion
	#region [ Properties ]
	public IProductServiceProviders Products { get; set; }
    public ICategoryServiceProviders Categories { get; }
    public IOrderServiceProviders Orders { get; }
    public IOrderDetailServiceProviders OrdersDetails { get; }
    public IMemberServiceProviders Members { get; }
    #endregion
}
