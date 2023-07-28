using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class ProductForm
{
    #region [ Methods - Property ]
    [Parameter]
    public int? ProductId { get; set; }

    [Inject]
    private ServiceContext ServiceContext { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    private List<Category> Categories { get; set; }
    private Category SelectedCategory { get; set; }

    private Product Product { get; set; }

    private string Role { get; set; } = string.Empty;
    #endregion

    #region [ Methods - override ]
    protected async override Task OnInitializedAsync() {
        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }

        this.Categories = await this.ServiceContext.Categories.GetListAllAsync();

        StateHasChanged();
           
        StateHasChanged();
    }

    protected override async Task OnParametersSetAsync() {
        if (ProductId == null || ProductId == 0) {
            this.Product = new Product();
        } else {
            this.Product = await this.ServiceContext.Products.GetSingleProductByIdAsync(ProductId.Value);
            await this.AddCategoryAsync(this.Product);
        }
        this.SelectedCategory = this.Product.Category;
        await base.OnParametersSetAsync();    
    }
    #endregion

    #region [ Private Methods -  ]
    private async Task AddCategoryAsync(Product product) {

        Product.Category = await this.ServiceContext.Categories.GetSingleByIdAsync(product.CategoryId.Value);

    }

    public async Task DeleteAsync(int productId) {

    }
    public async Task UpdateAsync(int productId) {

    }
    
    #endregion
}
