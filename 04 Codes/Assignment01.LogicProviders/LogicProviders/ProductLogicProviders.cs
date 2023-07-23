using Assignment01.DataProviders;
using Assignment01.EntityProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

    public async Task<List<Product>> GetListByCategoryIdAsync(int categoryId) {
        if (categoryId == null) {
            return null;
        }

        return await this._dataProvider.GetListByCategoryIdAsync(categoryId);
    }

    public async Task<List<Product>> GetListBySearchStringAsync(string searchString) {
        //if (searchString.IsNullOrEmpty()) {
        //    return null;
        //}

        return await this._dataProvider.GetListBySearchStringAsync(searchString);
    }

    public async Task<List<Product>> GetListByUnitPriceRangeAsync(decimal fromPrice, decimal toPrice) {

        return await this._dataProvider.GetListByUnitPriceRangeAsync(fromPrice, toPrice);
    }
    #endregion
}
