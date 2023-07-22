using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.DataProviders;

public class DataContext
{
    #region [ Properties ]
    public ICategoryDataProviders Category { get; set; }

    public IMemberDataProviders Member { get; set; }

    public IOrderDetailDataProviders OrderDetail { get; set; }

    public IOrderDataProviders Order { get; set; }

    public IProductDataProviders Product { get; set; }
    #endregion

    #region [ CTor ]
    public DataContext(ICategoryDataProviders Category,
                        IMemberDataProviders member,
                        IOrderDetailDataProviders orderDetail,
                        IOrderDataProviders order,
                        IProductDataProviders product) {
        this.Category = Category;
        this.Member = member;
        this.Order = order;
        this.OrderDetail = orderDetail;
        this.Product = product;
    }
    #endregion
}
