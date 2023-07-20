using Assignment01.DataProviders;
using Assignment01.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public class ProductLogicProviders : BaseEntityLogicProvider<Product, IProductDataProviders>, IProductLogicProviders
{
    #region [ CTor ]
    public ProductLogicProviders(ILogger<BaseEntityLogicProvider<Product, IProductDataProviders>> logger, IProductDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Product> GetSingleByIdAsync(int id) {
        if (id == null) {
            return null;
        }
        return await this._dataProvider.GetSingleByIdAsync(id);
    }
    #endregion
}
