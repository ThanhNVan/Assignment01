using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public interface IOrderDetailLogicProviders :  IBaseEntityLogicProvider<OrderDetail>
{
    Task<OrderDetail> GetSingleByOrderIdAndProductIdAsync(int orderId, int productId);
}
