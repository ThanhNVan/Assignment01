using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace SharedLibraries;

public abstract class BaseEntityDataProvider<TEntity, TContext> : IBaseEntityDataProvider<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    #region [ Fields ]
    protected readonly ILogger<BaseEntityDataProvider<TEntity, TContext>> _logger;

    protected readonly IDbContextFactory<TContext> _dbContextFactory;
    #endregion

    #region [ CTor ]
    public BaseEntityDataProvider(ILogger<BaseEntityDataProvider<TEntity, TContext>> logger, IDbContextFactory<TContext> dbContextFactory) {
        this._logger = logger;
        this._dbContextFactory = dbContextFactory;
    }
    #endregion

    public async virtual Task<List<TEntity>> GetListAllAsync() {
        try {
            using (TContext context = GetContext()) {
                return await EntityFrameworkQueryableExtensions.ToListAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<TEntity>()));
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            throw;
        }
    }

    public async virtual Task<bool> UpdateAsync(TEntity entity) {
        try {
            using (TContext context = GetContext()) {
                context.Update(entity);
                await context.SaveChangesAsync();
                return true;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return false;
        }
    }

    public async virtual Task<PagingResult<TEntity>> GetListAllAsync(PagingOptions options) {
        try {
            PagingResult<TEntity> result = new PagingResult<TEntity>();
            using (TContext context = GetContext()) {
                if (options.LoadStrategy == PagingLoadStrategy.PerRequest) {
                    if (options.CalculateTotals) {
                        int num2 = (result.TotalItems = await EntityFrameworkQueryableExtensions.
                                                        CountAsync(EntityFrameworkQueryableExtensions.
                                                        AsNoTracking(context.Set<TEntity>())));
                        result.TotalPages = num2 / options.PageSize + ((num2 % options.PageSize > 0) ? 1 : 0);
                    }

                    List<TEntity> collection = await EntityFrameworkQueryableExtensions.
                                            ToListAsync(EntityFrameworkQueryableExtensions.
                                            AsNoTracking(context.Set<TEntity>()).
                                            Paging(options.Page, options.PageSize));
                    result.Items.AddRange(collection);
                    result.Page = options.Page;
                    result.LoadStrategy = options.LoadStrategy;
                    result.IsFinishedLoading = true;

                } else {
                    int num4 = (result.TotalItems = await EntityFrameworkQueryableExtensions.CountAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<TEntity>())));
                    result.TotalPages = num4 / options.PageSize + ((num4 % options.PageSize > 0) ? 1 : 0);
                    List<TEntity> collection2 = await EntityFrameworkQueryableExtensions.ToListAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<TEntity>()).Paging(options.Page, options.PageSize));
                    result.Items.AddRange(collection2);
                    result.Page = options.Page;
                    result.LoadStrategy = options.LoadStrategy;
                    result.IsFinishedLoading = result.Page >= result.TotalPages;
                }
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            throw;
        }
    }



    public async virtual Task<bool> AddAsync(TEntity entity) {
        try {
            using (TContext context = GetContext()) {
                //if (await context.Set<TEntity>().FindAsync(entity.Id) != null) {
                //    this._logger.LogWarning("Duplicated Entity");
                //    return false;
                //}

                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return false;
        }
    }

    public async virtual Task<int> CountAllAsync() {
        try {
            using (TContext context = GetContext()) {
                return await EntityFrameworkQueryableExtensions.CountAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<TEntity>()));
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            throw;
        }
    }

    public async virtual Task<bool> DeleteAsync(TEntity entity) {
        try {
            if (entity == null) {
                return false;
            }
            using (TContext context = GetContext()) {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return false;
        }
    }

    protected TContext GetContext() {
        return this._dbContextFactory.CreateDbContext();
    }
}
