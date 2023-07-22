using Assignment01.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.DataProviders;

public class CategoryDataProviders : BaseEntityDataProvider<Category, AppDbContext>, ICategoryDataProviders
{
    #region [ CTor ]
    public CategoryDataProviders(ILogger<BaseEntityDataProvider<Category, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Category> GetSingleByIdAsync(int id) {
        var result = default(Category);
        try {
            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.
                    FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Category>()), (Category x) => x.CategoryId == id);
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
