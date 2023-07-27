using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class ProductPage
{
    #region [ Properties ]
    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    public ServiceContext ServiceContext { get; set; }

    private string Role { get; set; } = string.Empty;

    private bool IsAuthorized { get; set; } = false;

    public List<Product> ProductList { get; set; }
    #endregion

    #region [ Methods - OnInitializedAsync ]
    protected override async Task OnInitializedAsync() {
        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }
        StateHasChanged();

        if (!Role.IsNullOrEmpty()) {
            this.IsAuthorized = true;
            this.ProductList = await this.ServiceContext.Products.GetListAllProductsAsync();
            await this.AddCategoryAsync(this.ProductList);
        }
        StateHasChanged();
    }
    #endregion

    #region [ Private Methods -  ]
    private async Task AddCategoryAsync(List<Product> products) {
        foreach (var item in products) {
            item.Category = await this.ServiceContext.Categories.GetSingleByIdAsync(item.CategoryId.Value);
        }
    }
    #endregion
}
