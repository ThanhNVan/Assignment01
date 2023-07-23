using Assignment01.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.DataProviders;

public class OrderDataProviders : BaseEntityDataProvider<Order, AppDbContext>, IOrderDataProviders
{
    #region [ CTor ]
    public OrderDataProviders(ILogger<BaseEntityDataProvider<Order, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Order> GetSingleByIdAsync(int id) {
        var result = default(Order);
        try {
            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Order>()), (Order x) => x.OrderId == id);
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<List<Order>> GetListByMemberIdAsync(int memberId) {
        var result = default(List<Order>);
        try {

            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.ToListAsync(EntityFrameworkQueryableExtensions
                    .AsNoTracking(from x in context.Set<Order>()
                                  where x.MemberId == memberId
                                  select x));
                return result;
            }

        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
