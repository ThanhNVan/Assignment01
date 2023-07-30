using Assignment01.DataProviders;
using Assignment01.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public class OrderDetailLogicProviders : BaseEntityLogicProvider<OrderDetail, IOrderDetailDataProviders>, IOrderDetailLogicProviders
{
    #region [ CTor ]
    public OrderDetailLogicProviders(ILogger<BaseEntityLogicProvider<OrderDetail, IOrderDetailDataProviders>> logger, IOrderDetailDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<OrderDetail> GetSingleByOrderIdAndProductIdAsync(int orderId, int productId) {
        if (orderId == null || productId == null) {
            return null;
        }
        return await this._dataProvider.GetSingleByOrderIdAndProductIdAsync(orderId, productId);
    }

    public async Task<List<OrderDetail>> GetListByOrderIdAsync(int orderId) {
        var result = default(List<OrderDetail>);
        
        if (orderId == null) {
            return result;
        }

        return await this._dataProvider.GetListByOrderId(orderId);
    }
    #endregion
}
