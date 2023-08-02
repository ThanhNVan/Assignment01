using Assignment01.EntityProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface IProductServiceProviders
{
    Task<List<Product>> GetListAllProductsAsync();

    Task<IEnumerable<Product>> GetListByCategoryIdAsync(int categoryId);

    Task<Product> GetSingleProductByIdAsync(int productId);

    Task<bool> AddAsync(Product product); 

    Task<bool> DeleteAsync(int productId);

    Task<bool> UpdateAsync(Product product);


}
