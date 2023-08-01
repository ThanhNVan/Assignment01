using Assignment01.EntityProviders;

namespace Assignment01.BlazorWebApp;

public class CartRow
{
	#region [ Properties ]
	public Product Product { get; set; }

	public int Quantity { get; set; } = 0;
	#endregion
}
