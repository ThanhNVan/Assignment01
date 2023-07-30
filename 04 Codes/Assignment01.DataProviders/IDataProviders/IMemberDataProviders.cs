using Assignment01.EntityProviders;
using SharedLibraries;

namespace Assignment01.DataProviders;

public interface IMemberDataProviders : IBaseEntityDataProvider<Member>
{
    Task<Member> GetSingleByIdAsync(int id);

    Task<Member> GetSingleByEmailAsync(string email);

    Task<Member> LoginAsync(string email, string password);
}
