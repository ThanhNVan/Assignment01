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

public partial class MemberProductPage
{
    #region [ Fields ]
    private string _searchName = string.Empty;
    private string _searchFromPrice;
    private string _searchToPrice;
    private List<Product> _productListInit;
    public List<CartRow> _cartRowList;
    #endregion

    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    public ServiceContext ServiceContext { get; set; }

    private string Role { get; set; } = string.Empty;

    public List<CartRow> CartRowList { get; set; }
    public IQueryable<CartRow> DisplayCartRow { get; set; }

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
        set {
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
            this._productListInit = await this.ServiceContext.Products.GetListAllProductsAsync();
            this._cartRowList = new List<CartRow>();
            for (int i = 0; i < this._productListInit.Count; i++) {
                var cartRow = new CartRow() {
                    Product = this._productListInit[i],
                    Quantity = 0
                };
                this._cartRowList.Add(cartRow);
            }

            foreach (var item in this._cartRowList) {

                await this.AddCategoryAsync(item.Product);
            }
            this.CartRowList = this._cartRowList;
            this.DisplayCartRow = this._cartRowList.AsQueryable();
        }
        StateHasChanged();
    }
    #endregion

    #region [ Methods -  ]
    private void Search() {
        //var isSearchName = !string.IsNullOrEmpty(SearchName);
        var isFromPrice = decimal.TryParse(SearchFromPrice, out decimal fromPrice);
        var isToPrice = decimal.TryParse(SearchToPrice, out decimal toPrice);

        if (isFromPrice && isToPrice) {

            this.DisplayCartRow = this.CartRowList.Where(x => x.Product.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)
                                    && fromPrice <= x.Product.UnitPrice
                                    && x.Product.UnitPrice <= toPrice).AsQueryable();
        }

        if (isFromPrice) {
            this.DisplayCartRow = this.CartRowList.Where(x => fromPrice <= x.Product.UnitPrice
                                && x.Product.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();
            return;
        }

        if (isToPrice) {
            this.DisplayCartRow = this.CartRowList.Where(x => x.Product.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)
                                    && x.Product.UnitPrice <= toPrice).AsQueryable();
            return;
        } else {

            this.DisplayCartRow = this.CartRowList.Where(x => x.Product.ProductName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();
        }

    }

    public async Task CheckoutAsync() {
        var rdm = new Random();
        var orderId = (await this.ServiceContext.Orders.GetListAllAsync()).Count() + rdm.Next();
        var memberId = await this.SessionStorage.GetItemAsync<int>(AppRole.MemberId);
        var freight = decimal.Parse(this.CartRowList.Sum(x => x.Quantity).ToString());

        var order = new Order() {
            OrderId = orderId,
            MemberId = memberId,
            OrderDate = DateTime.UtcNow,
            RequiredDate = DateTime.UtcNow.AddDays(10),
            ShippedDate = DateTime.UtcNow.AddDays(10),
            Freight = freight
        };

        var response = await this.ServiceContext.Orders.AddOrderAsync(order);

        if (response != null) {
            foreach (var item in this.CartRowList) {
                if (item.Quantity > 0) {
                    var orderDetail = new OrderDetail() {
                        OrderId = response.OrderId,
                        ProductId = item.Product.ProductId,
                        UnitPrice = item.Product.UnitPrice,
                        Quantity = item.Quantity,
                        Discount = 5
                    };
                    await this.ServiceContext.OrdersDetails.AddAsync(orderDetail);
                }
            }
        }

        await Task.Delay(500);
        this.NavigationManager.NavigateTo("/MyOrders");
    }

    public void AddToCart(CartRow cart) {
        this.CartRowList.Where(x => x.Product.ProductId == cart.Product.ProductId).FirstOrDefault().Quantity++;
        if (this.CartRowList.Where(x => x.Product.ProductId == cart.Product.ProductId).FirstOrDefault().Quantity > cart.Product.UnitsInStock) {
            this.CartRowList.Where(x => x.Product.ProductId == cart.Product.ProductId).FirstOrDefault().Quantity = cart.Product.UnitsInStock;
        }
        this.DisplayCartRow = this.CartRowList.AsQueryable();
    }

    public void RemoveFromCart(CartRow cart) {
        this.CartRowList.Where(x => x.Product.ProductId == cart.Product.ProductId).FirstOrDefault().Quantity--;

        if (this.CartRowList.Where(x => x.Product.ProductId == cart.Product.ProductId).FirstOrDefault().Quantity < 0) {
            this.CartRowList.Where(x => x.Product.ProductId == cart.Product.ProductId).FirstOrDefault().Quantity = 0;
        }
        this.DisplayCartRow = this.CartRowList.AsQueryable();
    }
    #endregion

    #region [ Methods -  ]
    private async Task AddCategoryAsync(Product products) {

        products.Category = await this.ServiceContext.Categories.GetSingleByIdAsync(products.CategoryId.Value);

    }
    #endregion
}
