namespace SharedLibraries;

public interface IBaseEntityLogicProvider<TEntity> : IBaseEntityDataProvider<TEntity> where TEntity : class
{
}
