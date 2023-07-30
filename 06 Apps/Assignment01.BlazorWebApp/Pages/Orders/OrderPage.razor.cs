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

public partial class OrderPage
{
    #region [ Fields ]
    private string _searchEmail;
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

    public IQueryable<Order> OrderList { get; set; }

    public string SearchName {
        get => this._searchEmail;
        set {
            this._searchEmail = value;
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
            this._orderList = await this.ServiceContext.Orders.GetListAllAsync();

            this.OrderList = this._orderList.AsQueryable();
        }
        await this.FillMembers(this.OrderList);
        StateHasChanged();
    }
    #endregion

    #region [ Public Methods -  ]
    public void ViewDetail(int orderId) {
        this.NavigationManager.NavigateTo($"/Orders/Detail/{orderId}");
    }

    public async Task DeleteAsync(int orderid) {
        var aa = orderid;
    }

    public async Task ReportAsync() {

    }
    #endregion

    #region [ Private Methods -  ]
    private void Search() {

        this.OrderList = this._orderList.Where(x => x.Member.Email.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();

    }

    private async Task FillMembers(IEnumerable<Order> orders) {
        foreach (var item in orders) {
            item.Member = await this.ServiceContext.Members.GetSingleByIdAsync(item.MemberId.Value);
        }
    }
    #endregion
}
