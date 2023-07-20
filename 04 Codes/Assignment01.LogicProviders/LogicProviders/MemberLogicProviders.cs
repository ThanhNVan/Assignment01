using Assignment01.DataProviders;
using Assignment01.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public class MemberLogicProviders : BaseEntityLogicProvider<Member, IMemberDataProviders>, IMemberLogicProviders
{
    #region [ CTor ]
    public MemberLogicProviders(ILogger<BaseEntityLogicProvider<Member, IMemberDataProviders>> logger, IMemberDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Member> GetSingleByIdAsync(int id) {
        if (id == null) {
            return null;
        }
        return await this._dataProvider.GetSingleByIdAsync(id);
    }
    #endregion
}
