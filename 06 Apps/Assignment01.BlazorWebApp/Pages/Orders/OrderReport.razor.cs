using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class OrderReport
{
    #region [ Fields ]
    private DateTime _startDate = DateTime.UtcNow;
    private DateTime _endDate = DateTime.UtcNow;
    #endregion
    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private ServiceContext ServiceContext { get; set; }

    private string Role { get; set; } = string.Empty;

    private IQueryable<ReportRow> ReportRows { get; set; } = null;

    private IQueryable<Order> Orders { get; set; } = null;

    private string Total { get; set; }

    public DateTime StartDate { 
        get => this._startDate; 
        set  {
            this._startDate = value;
        }
    }
    public DateTime EndDate {
        get => this._endDate;
        set {
            this._endDate = value;
        }
    }
    #endregion

    #region [ Methods - OnInitializedAsync ]

    protected async override Task OnInitializedAsync() {
        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }

        await base.OnInitializedAsync();
    }
    #endregion

    #region [ Private Methods -  ]
    private async Task GenerateReportAsync() {

        var orderDetailList = await this.ServiceContext.OrdersDetails.GetListByReportAsync(StartDate, EndDate);

        var productList = orderDetailList.Select(x => x.ProductId).Distinct();
        var result = new List<ReportRow>();

        foreach (var item in productList) {
            var reportRow = new ReportRow();
            reportRow.Id = item;
            reportRow.Product = await this.ServiceContext.Products.GetSingleProductByIdAsync(item);
            reportRow.Quantity =orderDetailList.Where(x => x.ProductId == item).Sum(y => y.Quantity);
            reportRow.TotalPrice = orderDetailList.Where(x => x.ProductId== item).Sum(y => (double.Parse(y.UnitPrice.ToString()) *  y.Quantity) *(1 - (y.Discount/100)));

            result.Add(reportRow); 
        }

        this.Total = "Total: " + result.Sum(x => x.TotalPrice).ToString() + " Vnd";

        this.ReportRows = result.AsQueryable();

        this.Orders = (await this.ServiceContext.Orders.GetListByDateRangeAsync(StartDate, EndDate)).AsQueryable();
        await this.FillMembers(this.Orders);
    }

    public void ViewDetail(int orderId) {
        this.NavigationManager.NavigateTo($"/Orders/Detail/{orderId}");
    }

    private async Task FillMembers(IEnumerable<Order> orders) {
        foreach (var item in orders) {
            item.Member = await this.ServiceContext.Members.GetSingleByIdAsync(item.MemberId.Value);
        }
    }
    #endregion
}
