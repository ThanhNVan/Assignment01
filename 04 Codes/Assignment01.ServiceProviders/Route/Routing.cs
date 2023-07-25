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
    public static string Add = "/add";
    public static string GetAll = "/all";
	public static string GetSingle = "/single/";
	public static string Update = "/update/";
	public static string Delete = "/delete/";
	public static string Search = "/search/";
	public static string ByCategoryId = "/bycategoryid/";
	public static string Login = "/login";
	public static string ByMemberId = "/ByMemberId/";
	public static string ByOrderId = "/ByOrderId/";
	#endregion
}
