using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Invoice GetInvoiceForUser(int invoiceId, int userId);
    }
}
