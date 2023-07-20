using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public interface ICategoryLogicProviders : IBaseEntityLogicProvider<Category>
{
    Task<Category> GetSingleByIdAsync(int id);
}
