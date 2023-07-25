namespace Assignment01.ServiceProviders;

public class ServiceContext
{
	#region [ CTor ]
	public ServiceContext(IProductServiceProviders products) {
		this.Products = products;	
	}
	#endregion
	#region [ Properties ]
	public IProductServiceProviders Products { get; set; }
	#endregion
}
