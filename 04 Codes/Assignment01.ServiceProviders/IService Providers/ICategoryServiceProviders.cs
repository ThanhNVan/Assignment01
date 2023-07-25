using Assignment01.EntityProviders;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface ICategoryServiceProviders
{
    Task<Category> GetSingleByIdAsync(int id);
}
