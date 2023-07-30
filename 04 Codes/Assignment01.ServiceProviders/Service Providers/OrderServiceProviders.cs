using Assignment01.EntityProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

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
    #endregion
}
