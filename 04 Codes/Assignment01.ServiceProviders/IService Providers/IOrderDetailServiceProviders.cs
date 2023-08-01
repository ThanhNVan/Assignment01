using Assignment01.EntityProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface IOrderDetailServiceProviders
{
    Task<IEnumerable<OrderDetail>> GetListByOrderIdAsync(int orderId); 

    Task<IEnumerable<OrderDetail>> GetListByReportAsync(DateTime startDate, DateTime endDate);
}
