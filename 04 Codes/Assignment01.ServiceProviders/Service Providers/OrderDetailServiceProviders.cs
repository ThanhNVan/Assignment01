using Assignment01.EntityProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public class OrderDetailServiceProviders : IOrderDetailServiceProviders
{
    #region [ Fields ]
    private readonly HttpClient _httpClient;

    #endregion

    #region [ CTor ]
    public OrderDetailServiceProviders(HttpClient httpClient) {
        this._httpClient = httpClient;
    }
    #endregion

    #region [ Methods -  ]
    public async Task<IEnumerable<OrderDetail>> GetListByOrderIdAsync(int orderId) {
        var result = default(IEnumerable<OrderDetail>);

        var url = Routing.BaseUrl + Routing.OrderDetailApi + Routing.ByOrderId + orderId.ToString();
        result = await this._httpClient.GetFromJsonAsync<IEnumerable<OrderDetail>>(url);

        return result;
    }

    public async Task<IEnumerable<OrderDetail>> GetListByReportAsync(DateTime startDate, DateTime endDate) {
        var result = default(IEnumerable<OrderDetail>);
        var dateList = new List<DateTime>() { startDate, endDate};

        var url = Routing.BaseUrl + Routing.OrderDetailApi + Routing.Report;

        var response = await this._httpClient.PostAsJsonAsync(url, dateList);

        if (response.StatusCode == HttpStatusCode.OK) {
            result = await JsonConvert.DeserializeObjectAsync<List<OrderDetail>>( await response.Content.ReadAsStringAsync());
        }

        return result;
    }
    #endregion
}
