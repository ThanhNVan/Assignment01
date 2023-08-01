using Assignment01.EntityProviders;

namespace Assignment01.BlazorWebApp;

public class ReportRow
{
	#region [ Properties ]
	public int Id { get; set; }

	public Product Product { get; set; }

	public int Quantity { get; set; }

	public double TotalPrice { get; set; }
	#endregion
}
