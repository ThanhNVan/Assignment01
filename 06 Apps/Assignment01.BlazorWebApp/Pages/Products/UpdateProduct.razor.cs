using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class UpdateProduct
{
    #region [ Fields ]
    private Category _selectedCategory;
    #endregion

    #region [ Properties ]
    [Parameter]
    public int? ProductId { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ServiceContext ServiceContext { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    private Category SelectedCategory {
        get => this._selectedCategory;
        set {
            this._selectedCategory = value;
            this.ChangeWeightUnit();
        }
    }

    private List<Category> Categories { get; set; }

    private Product Product { get; set; } = new Product();

    private string Role { get; set; } = string.Empty;

    private string WeightUnit { get; set; }

    private string InputWeight { get; set; }
    private string InputUnitPrice { get; set; }
    private string InputUnitInStock { get; set; }

    private string WarningInputUnitPrice { get; set; }
    private string WarningInputWeight { get; set; }
    private string WarningInputUnitInStock { get; set; }
    private string ResultDisplay { get; set; }
    #endregion

    protected async override Task OnInitializedAsync() {

        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }

        this.Categories = await this.ServiceContext.Categories.GetListAllAsync();
        this.Product = await this.ServiceContext.Products.GetSingleProductByIdAsync(this.ProductId.Value);
        this.SelectedCategory = this.Categories.Where(x => x.CategoryId == this.Product.CategoryId).FirstOrDefault();

        this.InputWeight = this.Product.Weight.Replace("g", "").Replace("ml", "");
        this.InputUnitPrice = string.Format("{0:n2}", this.Product.UnitPrice.ToString());
        this.InputUnitInStock = this.Product.UnitsInStock.ToString();

        this.StateHasChanged();
        await base.OnInitializedAsync();
    }

    #region [ Methods -  ]
    public async Task UpdateProductAsync() {
        var isValidWeight = decimal.TryParse(InputWeight, out var weight);
        if (!isValidWeight) {
            this.WarningInputWeight = "Not Valid weight, Please try again";

            return;
        }

        var isValidUnitPrice = decimal.TryParse(InputUnitPrice, out var unitPrice);
        if (!isValidUnitPrice) {
            this.WarningInputUnitPrice = "Not Valid Price, Please try again";

            return;
        }

        var isValidUnitInStock = int.TryParse(InputUnitInStock, out var unitInStock);
        if (!isValidUnitInStock) {
            this.WarningInputUnitInStock = "Not Valid Unit in stock, Please try again";
            return;
        }
        var rdm = new Random();

        this.Product.CategoryId = this.SelectedCategory.CategoryId;
        this.Product.Weight = weight.ToString() + this.WeightUnit;
        this.Product.UnitPrice = unitPrice;
        this.Product.UnitsInStock = unitInStock;

        var result = await this.ServiceContext.Products.UpdateAsync(this.Product);

        if (result) {
            this.ResultDisplay = "Updated";
            await Task.Delay(500);
            this.NavigationManager.NavigateTo("/Products");
        } else {
            this.ResultDisplay = "Failed to update";
        }
    }
    #endregion

    #region [ Private Methods ]
    private void ChangeWeightUnit() {
        if (this.SelectedCategory.CategoryName == "Food") {
            this.WeightUnit = "g";
        } else {
            this.WeightUnit = "ml";
        }
    }
    #endregion
}

