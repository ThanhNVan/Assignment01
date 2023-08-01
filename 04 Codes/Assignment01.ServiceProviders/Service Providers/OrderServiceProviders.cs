using Assignment01.EntityProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public class OrderServiceProviders : IOrderServiceProviders
{
    #region [ Fields ]
    private readonly HttpClient _httpClient;

    #endregion

    #region [ CTor ]
    public OrderServiceProviders(HttpClient httpClient) {
        this._httpClient = httpClient;
    }
    #endregion

    #region [ Methods - CRUD ]
    public async Task<Order> GetSingleByIdAsync(int id) {
        var result = default(Order);

        var url = Routing.BaseUrl + Routing.OrderApi + Routing.GetSingle + id.ToString();

        result = await this._httpClient.GetFromJsonAsync<Order>(url);

        return result;
    }

    public async Task<List<Order>> GetListAllAsync() {
        var result = default(List<Order>);

        var url = Routing.BaseUrl + Routing.OrderApi + Routing.GetAll;

        result = await this._httpClient.GetFromJsonAsync<List<Order>>(url);

        return result;
    }

    public async Task<IEnumerable<Order>> GetListByDateRangeAsync(DateTime startDate, DateTime endDate) {
        var result = default(List<Order>);

        var dateList = new List<DateTime>() { startDate, endDate };

        var url = Routing.BaseUrl + Routing.OrderApi + Routing.Report;

        var response = await this._httpClient.PostAsJsonAsync(url, dateList);

        if (response.StatusCode == HttpStatusCode.OK) {
            result = await JsonConvert.DeserializeObjectAsync<List<Order>>(await response.Content.ReadAsStringAsync());
        }

        return result;
    }
    #endregion
}
