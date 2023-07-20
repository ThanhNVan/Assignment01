using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.DataProviders;

public interface IProductDataProviders : IBaseEntityDataProvider<Product>
{
    Task<Product> GetSingleByIdAsync(int id);
}
