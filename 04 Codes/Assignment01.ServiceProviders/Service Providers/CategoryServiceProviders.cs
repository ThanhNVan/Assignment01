using Assignment01.EntityProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public class CategoryServiceProviders : ICategoryServiceProviders
{
    #region [ Fields ]
    private readonly HttpClient _httpClient;

    #endregion

    #region [ CTor ]
    public CategoryServiceProviders(HttpClient httpClient) {
        this._httpClient = httpClient;
    }
    #endregion

    #region [ Methods - CRUD ]
    public async Task<Category> GetSingleByIdAsync(int id) {
        var result = await this._httpClient.GetFromJsonAsync<Category>(Routing.BaseUrl + Routing.CategoryApi + Routing.GetSingle + id.ToString());

        return result;  
    }

    public async Task<List<Category>> GetListAllAsync() {
        var result = await this._httpClient.GetFromJsonAsync<List<Category>>(Routing.BaseUrl + Routing.CategoryApi + Routing.GetAll);
        return result;
    }
    #endregion
}
