using Assignment01.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibraries;

namespace Assignment01.DataProviders;

public class MemberDataProviders : BaseEntityDataProvider<Member, AppDbContext>, IMemberDataProviders
{
    #region [ CTor ]
    public MemberDataProviders(ILogger<BaseEntityDataProvider<Member, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Member> GetSingleByIdAsync(int id) {
        var result = default(Member);
        try {
            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Member>()), (Member x) => x.MemberId == id);
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<Member> GetSingleByEmailAsync(string email) {
        var result = default(Member);

        try {
            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Member>()), (Member x) => x.Email == email);
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<Member> LoginAsync(string email, string password) {
        var result = default(Member);
        try {
            using (var context = this.GetContext()) {
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<Member>()), (Member x) => x.Email == email && x.Password == password);
                return result;
            }
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
