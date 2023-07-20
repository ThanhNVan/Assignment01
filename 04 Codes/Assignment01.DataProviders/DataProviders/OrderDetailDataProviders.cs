using Assignment01.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.DataProviders;

public class OrderDetailDataProviders : BaseEntityDataProvider<OrderDetail, AppDbContext>, IOrderDetailDataProviders
{
    #region [ CTor ]
    public OrderDetailDataProviders(ILogger<BaseEntityDataProvider<OrderDetail, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<OrderDetail> GetSingleByOrderIdAndProductIdAsync(int orderId, int productId) {
        var result = default(OrderDetail);
        try {
            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions
                    .AsNoTracking(context.Set<OrderDetail>()), (OrderDetail x) => x.OrderId == orderId
                     && x.ProductId == productId);
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
