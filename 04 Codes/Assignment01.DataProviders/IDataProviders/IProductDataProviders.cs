using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.DataProviders;

public interface IProductDataProviders : IBaseEntityDataProvider<Product>
{
    Task<Product> GetSingleByIdAsync(int id);

    // Task<List<Product>> GetListByMemberId(int memberId);

    // Task<List<Product>> GetListByOrderId(int orderId);

    Task<List<Product>> GetListByCategoryIdAsync(int categoryId);

    Task<List<Product>> GetListBySearchStringAsync(string searchString);

    Task<List<Product>> GetListByUnitPriceRangeAsync(decimal fromPrice, decimal toPrice);
}
