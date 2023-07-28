using Assignment01.EntityProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface IProductServiceProviders
{
    Task<List<Product>> GetListAllProductsAsync();

    Task<Product> GetSingleProductByIdAsync(int productId);
    Task<bool> AddAsync(Product product); 
}
