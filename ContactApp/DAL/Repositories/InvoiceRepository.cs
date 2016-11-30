using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class InvoiceRepository : EFRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Invoice GetInvoiceForUser(int invoiceId, int userId)
        {
            var res =
                DbContext.Set<Order>().FirstOrDefault(i => i.OrderUserId == userId && i.Invoice.InvoiceId == invoiceId);
            return res != null ? res.Invoice : null;
        }
    }
}
