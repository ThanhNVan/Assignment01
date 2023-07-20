using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public interface IProductLogicProviders : IBaseEntityLogicProvider<Product>
{
    Task<Product> GetSingleByIdAsync(int id);
}
