using Assignment01.DataProviders;
using Assignment01.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.LogicProviders;

public class OrderLogicProviders : BaseEntityLogicProvider<Order, IOrderDataProviders>, IOrderLogicProviders
{
    #region [ CTor ]
    public OrderLogicProviders(ILogger<BaseEntityLogicProvider<Order, IOrderDataProviders>> logger, IOrderDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Order> GetSingleByIdAsync(int id) {
        if (id == null) {
            return null;
        }
        return await this._dataProvider.GetSingleByIdAsync(id);
    }
    #endregion
}
