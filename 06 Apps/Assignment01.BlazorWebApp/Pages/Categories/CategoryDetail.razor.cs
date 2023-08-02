using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class CategoryDetail
{
    #region [ Fields ]
    private IEnumerable<Product> _productListInit;

    #endregion
    #region [ Properties ]
    [Parameter]
	public int CategoryId { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    public ServiceContext ServiceContext { get; set; }

    public IQueryable<Product> ProductList { get; set; }
    public Category Category { get; set; }
    #endregion
    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync() {
            this.Category = await this.ServiceContext.Categories.GetSingleByIdAsync(this.CategoryId);
            this._productListInit = await this.ServiceContext.Products.GetListByCategoryIdAsync(this.CategoryId);
            await this.AddCategoryAsync(this._productListInit);
            this.ProductList = this._productListInit.AsQueryable();

        StateHasChanged();
    }
    #endregion

    #region [ Methods -  ]
    public void Detail(int productId) {
        this.NavigationManager.NavigateTo($"/Products/{productId}");
    }
    #endregion

    #region [ Methods -  ]
    private async Task AddCategoryAsync(IEnumerable<Product> products) {
        foreach (var item in products) {
            item.Category = await this.ServiceContext.Categories.GetSingleByIdAsync(item.CategoryId.Value);
        }
    }
    #endregion
}
