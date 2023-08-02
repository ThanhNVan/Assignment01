using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class MyOrdersPage
{
    #region [ Fields ]
    private IEnumerable<Order> _orderList;
    #endregion

    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    public ServiceContext ServiceContext { get; set; }

    private string Role { get; set; } = string.Empty;

    public List<Order> OrderList { get; set; }
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
            var memberId = await this.SessionStorage.GetItemAsync<int>(AppRole.MemberId);
            this._orderList = await this.ServiceContext.Orders.GetListByMemberIdAsync(memberId);
            await this.FillOrderDetailsAsync(this._orderList);
            this.OrderList = this._orderList.ToList();
        }
        StateHasChanged();
    }
    #endregion


    #region [ Public Methods -  ]
    public void ViewDetail(int orderId) {
        this.NavigationManager.NavigateTo($"/Orders/Detail/{orderId}");
    }
    #endregion

    #region [ Private Methods -  ]
    private async Task FillOrderDetailsAsync(IEnumerable<Order> orders) {
        foreach (var item in orders) {
            item.OrderDetails = (await this.ServiceContext.OrdersDetails.GetListByOrderIdAsync(item.OrderId)).ToList();
            if (item.OrderDetails.Count > 0) {
                await this.FillProductsAsync(item.OrderDetails);
            }
        }
    }

    private async Task FillProductsAsync(IEnumerable<OrderDetail> orderDetails) {
        foreach(var item in orderDetails) {
            item.Product = await this.ServiceContext.Products.GetSingleProductByIdAsync(item.ProductId);
        }
    }
    #endregion
}
