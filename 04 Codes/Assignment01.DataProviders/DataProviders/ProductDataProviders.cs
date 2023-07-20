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
    #endregion
}
