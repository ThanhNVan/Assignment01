using Assignment01.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.DataProviders;

public class ProductDataProviders : BaseEntityDataProvider<Product, AppDbContext>, IProductDataProviders
{
    #region [ CTor ]
    public ProductDataProviders(ILogger<BaseEntityDataProvider<Product, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Product> GetSingleByIdAsync(int id) {
        var result = default(Product);
        try {
            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Product>()), (Product x) => x.ProductId == id);
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<List<Product>> GetListByCategoryIdAsync(int categoryId) {
        var result = default(List<Product>);
        try {
            using (var context = this.GetContext()) {
                return await EntityFrameworkQueryableExtensions.ToListAsync(from x in EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Product>())
                                                                            where x.CategoryId == categoryId
                                                                            select x);
            }

        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<List<Product>> GetListBySearchStringAsync(string searchString) {
        var result = default(List<Product>);
        try {
            using (var context = this.GetContext()) {
                return await EntityFrameworkQueryableExtensions.ToListAsync(from x in EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Product>())
                                                                            where x.ProductName.Contains(searchString)
                                                                            select x);
            }

        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<List<Product>> GetListByUnitPriceRangeAsync(decimal fromPrice, decimal toPrice) {
        var result = default(List<Product>);
        try {
            using (var context = this.GetContext()) {
                result =  await EntityFrameworkQueryableExtensions.ToListAsync(from x in EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Product>())
                                                                            where x.UnitPrice >= fromPrice
                                                                            && x.UnitPrice <= toPrice
                                                                            select x);

                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
