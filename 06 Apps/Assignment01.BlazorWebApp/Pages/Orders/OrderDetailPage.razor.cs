using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class OrderDetailPage
{
    #region [ Methods - Property ]
    [Parameter]
    public int? OrderId { get; set; }

    [Inject]
    private ServiceContext ServiceContext { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private IQueryable<OrderDetail> OrderDetailList { get; set; }
    
    private Order Order { get; set; }
    private Member Member{ get; set; }

    private string Role { get; set; } = string.Empty;

    private string DisplayOrderedDate { get; set; }
    private string DisplayRequiredDate { get; set; }
    private string DisplayShippedDate { get; set; }
    private string DisplayFreight { get; set; }

    #endregion

    #region [ Methods - override ]
    protected async override Task OnInitializedAsync() {
        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }

        this.Order = await this.ServiceContext.Orders.GetSingleByIdAsync(this.OrderId.Value);
        this.Member = await this.ServiceContext.Members.GetSingleByIdAsync(this.Order.MemberId.Value);
        this.OrderDetailList = (await this.ServiceContext.OrdersDetails.GetListByOrderIdAsync(this.OrderId.Value)).AsQueryable();
        await this.FillProductsAsync(this.OrderDetailList);

        this.DisplayOrderedDate = Order.OrderDate.ToShortDateString();
        this.DisplayRequiredDate = Order.RequiredDate.Value.ToShortDateString();
        this.DisplayShippedDate = Order.ShippedDate.Value.ToShortDateString();
        this.DisplayFreight = string.Format("{0:n2}", Order.Freight);

        await base.OnInitializedAsync();
    }
    #endregion

    #region [ Public Methods -  ]
    public async Task DeleteAsync(int orderId) {

    }
    #endregion
    
    #region [ Private Methods -  ]
    public async Task FillProductsAsync(IEnumerable<OrderDetail> orderDetails) {
        foreach (var item in orderDetails) {
            item.Product = await this.ServiceContext.Products.GetSingleProductByIdAsync(item.ProductId);
        }
    }
    #endregion
}
