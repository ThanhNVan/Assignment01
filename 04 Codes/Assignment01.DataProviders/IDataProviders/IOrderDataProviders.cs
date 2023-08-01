using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.DataProviders;

public interface IOrderDataProviders : IBaseEntityDataProvider<Order>
{
    Task<Order> GetSingleByIdAsync(int id);

    Task<List<Order>> GetListByMemberIdAsync(int memberId);

    Task<List<Order>> GetListByDateRangeAsync(DateTime startDate, DateTime endDate);
}
