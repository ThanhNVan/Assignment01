using Assignment01.EntityProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface IProductServiceProviders
{
    Task<List<Product>> GetListAllProductsAsync();

    Task<Product> GetSingleProductByIdAsync();
}
