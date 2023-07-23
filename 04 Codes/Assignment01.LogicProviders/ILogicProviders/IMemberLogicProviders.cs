using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public interface IMemberLogicProviders : IBaseEntityLogicProvider<Member>
{
    Task<Member> GetSingleByIdAsync(int id);

    Task<Member> LoginAsync(string email, string password);
}
