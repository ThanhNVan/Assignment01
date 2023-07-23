using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public interface IOrderLogicProviders : IBaseEntityLogicProvider<Order> 
{
    Task<Order> GetSingleByIdAsync(int id);

    Task<List<Order>> GetListByMemberIdAsync(int memberId);
}
