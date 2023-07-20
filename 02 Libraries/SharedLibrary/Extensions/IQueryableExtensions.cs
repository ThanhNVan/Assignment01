using System.Linq.Expressions;

namespace SharedLibraries;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> Paging<TEntity>(this IQueryable<TEntity> query, int page, int pageSize = 0) where TEntity : class {
        if (pageSize == 0) {
            return query;
        }

        if (page == 0) {
            return query.Take(pageSize);
        }

        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<TEntity> Paging<TEntity>(this IQueryable<TEntity> query, PagingOptions options) where TEntity : class {
        Guard.ParamIsNull(options, "options", "", "Paging");
        return query.Paging(options.Page, options.PageSize);
    }

    //public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, List<Expression<Func<TEntity, object>>> keySelectors) where TEntity : class {
    //    if (keySelectors != null && keySelectors.Count > 0) {
    //        foreach (Expression<Func<TEntity, object>> keySelector in keySelectors) {
    //            query = query.AppendOrderBy(keySelector);
    //        }

    //        query = query.AppendOrderBy((TEntity x) => x.Id);
    //    }

    //    return query;
    //}

    private static IOrderedQueryable<TEntity> AppendOrderBy<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, object>> keySelector) where TEntity : class {
        if (!(query.Expression.Type == typeof(IOrderedQueryable<TEntity>))) {
            return query.OrderBy(keySelector);
        }

        return ((IOrderedQueryable<TEntity>)query).ThenBy(keySelector);
    }

    private static IOrderedQueryable<TEntity> AppendOrderByDescending<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, object>> keySelector) where TEntity : class {
        if (!(query.Expression.Type == typeof(IOrderedQueryable<TEntity>))) {
            return query.OrderByDescending(keySelector);
        }

        return ((IOrderedQueryable<TEntity>)query).ThenByDescending(keySelector);
    }
}
