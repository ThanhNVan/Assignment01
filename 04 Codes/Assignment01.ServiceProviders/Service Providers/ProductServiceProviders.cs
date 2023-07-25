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
    public async Task<List<Product>> GetListAllProductsAsync() {
        var result = await this._httpClient.GetFromJsonAsync<List<Product>>(Routing.BaseUrl + Routing.ProductApi + Routing.GetAll);

        return result;
    }

    public async Task<Product> GetSingleProductByIdAsync() {
        throw new System.NotImplementedException();
    }

    
}
