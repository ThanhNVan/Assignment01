using Assignment01.DataProviders;
using Assignment01.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public class CategoryLogicProviders : BaseEntityLogicProvider<Category, ICategoryDataProviders>, ICategoryLogicProviders
{
    #region [ CTor ]
    public CategoryLogicProviders(ILogger<BaseEntityLogicProvider<Category, ICategoryDataProviders>> logger, ICategoryDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Category> GetSingleByIdAsync(int id) {
        if (id == null) {
            return null;
        }
        return await this._dataProvider.GetSingleByIdAsync(id);
    }
    #endregion
}
