using Assignment01.EntityProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
    #endregion
}
