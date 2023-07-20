using Microsoft.Extensions.Logging;

namespace SharedLibraries;

public abstract class BaseEntityLogicProvider<TEntity, TDataProvider> : IBaseEntityLogicProvider<TEntity>
    where TEntity : class
    where TDataProvider : IBaseEntityDataProvider<TEntity>
{
    #region [ Fields ]
    protected readonly ILogger<BaseEntityLogicProvider<TEntity, TDataProvider>> _logger;
    protected readonly TDataProvider _dataProvider;
    #endregion

    #region [ CTor ]
    public BaseEntityLogicProvider(ILogger<BaseEntityLogicProvider<TEntity, TDataProvider>> logger, TDataProvider dataProvider) {
        this._logger = logger;
        this._dataProvider = dataProvider;
    }
    #endregion

    public virtual async Task<List<TEntity>> GetListAllAsync() {
        return await this._dataProvider.GetListAllAsync();
    }

    public virtual async Task<PagingResult<TEntity>> GetListAllAsync(PagingOptions options) {
        Guard.IsNull(options, nameof(options));
        return await this._dataProvider.GetListAllAsync(options); 
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity) {
        Guard.IsNull(entity, nameof(entity));
        return await this._dataProvider.UpdateAsync(entity);
    }

    public virtual async Task<bool> AddAsync(TEntity entity) {
        Guard.IsNull(entity, nameof(entity));
        return await this._dataProvider.AddAsync(entity);
    }

    public virtual async Task<int> CountAllAsync() {
        return await this._dataProvider.CountAllAsync();
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity) {
        // need to implement logic here
        Guard.IsNull(entity, nameof(entity));
        return await this._dataProvider.DeleteAsync(entity);
    }
}
