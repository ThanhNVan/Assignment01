namespace SharedLibraries;
/// <summary>
/// CRUD only
/// </summary>
public interface IBaseEntityDataProvider<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetListAllAsync();

    Task<PagingResult<TEntity>> GetListAllAsync(PagingOptions options);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> AddAsync(TEntity entity);

    Task<int> CountAllAsync();

    Task<bool> DeleteAsync(TEntity entity);
}
