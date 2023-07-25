using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public static class Routing
{
	#region [ Properties - Base ]
	public static string BaseUrl = "https://localhost:7006/";
	public static string LocalHost = "http://localhost:5209";
	public static string ProductApi = "api/product";
	public static string CategoryApi = "api/category";
	public static string MemberApi = "api/member";
	public static string OrderApi = "api/order";
	public static string OrderDetailApi = "api/orderdetail";
	#endregion

	#region [ Properties - Methods ]
	public static string GetAll = "/all";
	public static string GetSingle = "/single/";
	public static string Delete = "/delete/";
	public static string Search = "/search/";
	#endregion
}
