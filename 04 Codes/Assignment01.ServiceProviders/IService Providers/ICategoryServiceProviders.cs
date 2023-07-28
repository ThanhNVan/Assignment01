using Assignment01.EntityProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface ICategoryServiceProviders
{
    Task<Category> GetSingleByIdAsync(int id);
    
    Task<List<Category>> GetListAllAsync();
}
