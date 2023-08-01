using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class ProductPage
{
    #region [ Fields ]
    private string _searchName;
    private string _searchFromPrice;
    private string _searchToPrice;
    private List<Product> _productListInit;
    #endregion

    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    public ServiceContext ServiceContext { get; set; }

    private string Role { get; set; } = string.Empty;

    public List<Product> ProductList { get; set; }

    public string SearchName { 
        get => this._searchName; 
        set { 
            this._searchName = value;
            this.Search();
        }
    }
    public string SearchFromPrice { 
        get => this._searchFromPrice;
        set { 
            this._searchFromPrice = value;
            this.Search();
        } 
    }
    public string SearchToPrice { 
        get => this._searchToPrice; 
        set{ 
            this._searchToPrice = value;
            this.Search();
        } 
    }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync() {
        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }
        StateHasChanged();

        if (!Role.IsNullOrEmpty()) {
            this.ProductList = await this.ServiceContext.Products.GetListAllProductsAsync();
            this._productListInit = this.ProductList;
            await this.AddCategoryAsync(this.ProductList);
        }
        StateHasChanged();
    }
    #endregion

    #region [ Private Methods -  ]
    private void Search() {
        //var isSearchName = !string.IsNullOrEmpty(SearchName);
        var isFromPrice = decimal.TryParse(SearchFromPrice, out decimal fromPrice);
        var isToPrice = decimal.TryParse(SearchToPrice, out decimal toPrice);

        if (isFromPrice && isToPrice) {

            this.ProductList = this._productListInit.Where(x => x.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)
                                    && fromPrice <= x.UnitPrice 
                                    && x.UnitPrice <= toPrice).ToList();
        }

        if (isFromPrice) {
            this.ProductList = this._productListInit.Where(x => fromPrice <= x.UnitPrice
                                && x.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return;
        }
        
        if (isToPrice) {
            this.ProductList = this._productListInit.Where(x => x.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)
                                    && x.UnitPrice <= toPrice).ToList();
            return;
        } else {

            this.ProductList = this._productListInit.Where(x => x.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

    }

    private async Task AddCategoryAsync(List<Product> products) {
        foreach (var item in products) {
            item.Category = await this.ServiceContext.Categories.GetSingleByIdAsync(item.CategoryId.Value);
        }
    }
    #endregion

    #region [ Public Methods -  ]
    public void ProductDetail(int productId) {
        NavigationManager.NavigateTo($"/Products/{productId}");
    }
    
    public void AddProductToCart(int productId) {

    }
    
    public async Task CheckOutAsync() {

    }
    
    public async Task DeleteAsync(int productId) {
        var result = await this.ServiceContext.Products.DeleteAsync(productId);
        StateHasChanged();
        await this.OnInitializedAsync();
    }
    public void Update(int productId) {
        this.NavigationManager.NavigateTo($"/Products/Update/{productId}");
    }
    public void Add() {
        this.NavigationManager.NavigateTo("/Products/New");
    }
    #endregion
}
