using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public interface IProductLogicProviders : IBaseEntityLogicProvider<Product>
{
    Task<Product> GetSingleByIdAsync(int id);

    Task<List<Product>> GetListByCategoryIdAsync(int categoryId);

    Task<List<Product>> GetListBySearchStringAsync(string searchString);

    Task<List<Product>> GetListByUnitPriceRangeAsync(decimal fromPrice, decimal toPrice);
}
