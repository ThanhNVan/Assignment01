using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.DataProviders;

public interface IOrderDetailDataProviders : IBaseEntityDataProvider<OrderDetail>
{
    Task<OrderDetail> GetSingleByOrderIdAndProductIdAsync(int orderId, int productId);

    Task<List<OrderDetail>> GetListByOrderId(int orderId);
}
