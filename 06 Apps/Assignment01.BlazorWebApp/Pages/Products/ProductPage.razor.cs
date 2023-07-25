using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class ProductPage
{
    #region [ Properties ]
    [Inject]
    public ServiceContext ServiceContext { get; set; }

    public List<Product> ProductList { get; set; }
    #endregion

    #region [ Methods - OnInitializedAsync ]
    protected override async Task OnInitializedAsync() {
        this.ProductList = await this.ServiceContext.Products.GetListAllProductsAsync();
        await this.AddCategoryAsync(this.ProductList);
    }
    #endregion

    #region [ Private Methods -  ]
    private async Task AddCategoryAsync(List<Product> products) {
        foreach (var item in products) {
           // item.Category = await this.ServiceContext.
        }
    }
    #endregion
}
