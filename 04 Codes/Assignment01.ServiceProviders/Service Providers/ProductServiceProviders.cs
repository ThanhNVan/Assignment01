using Assignment01.EntityProviders;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public class ProductServiceProviders : IProductServiceProviders
{
    #region [ Fields ]
    private readonly HttpClient _httpClient;

    #endregion

    #region [ CTor ]
    public ProductServiceProviders(HttpClient httpClient) {
        this._httpClient = httpClient;
    }
    #endregion

    #region [ Methods - CRUD ]

    public async Task<List<Product>> GetListAllProductsAsync() {
        var url = Routing.BaseUrl + Routing.ProductApi + Routing.GetAll;
        var result = await this._httpClient.GetFromJsonAsync<List<Product>>(url);

        return result;
    }

    public async Task<Product> GetSingleProductByIdAsync(int productId) {
        var url = Routing.BaseUrl + Routing.ProductApi + Routing.GetSingle + productId.ToString();
        var result = await this._httpClient.GetFromJsonAsync<Product>(url);

        return result;
    }
    #endregion    
}
