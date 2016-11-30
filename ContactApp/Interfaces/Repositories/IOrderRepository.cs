using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order GetOrderForUser(int orderId, int userId);

        List<Order> AllForUser(int userId);

        Order GetOrderByInvoiceId(int invoiceId);
    }
}
