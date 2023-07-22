using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.LogicProviders;

public class LogicContext
{
    #region [ Properties ]
    public ICategoryLogicProviders Category { get; set; }
    
    public IMemberLogicProviders Member { get; set; }

    public IOrderDetailLogicProviders OrderDetail { get; set; }

    public IOrderLogicProviders Order { get; set; }

    public IProductLogicProviders Product { get; set; }
    #endregion

    #region [ CTor ]
    public LogicContext(ICategoryLogicProviders Category,
                        IMemberLogicProviders member,
                        IOrderDetailLogicProviders orderDetail,
                        IOrderLogicProviders order, 
                        IProductLogicProviders product) {
        this.Category = Category;
        this.Member = member;
        this.Order = order;
        this.OrderDetail = orderDetail;
        this.Product = product;
    }
    #endregion
}
