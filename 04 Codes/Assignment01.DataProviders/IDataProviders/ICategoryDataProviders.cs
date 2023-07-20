using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.DataProviders;

public interface ICategoryDataProviders : IBaseEntityDataProvider<Category>
{
    Task<Category> GetSingleByIdAsync(int id);   
}
