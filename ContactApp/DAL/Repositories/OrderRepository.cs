using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Order GetOrderForUser(int orderId, int userId)
        {
            var res =
                DbSet.FirstOrDefault(o => o.OrderId == orderId && o.OrderUserId == userId);
            return res != null ? res : null;
        }

        public List<Order> AllForUser(int userId)
        {
            var res =
                DbSet.Where(o => o.OrderUserId == userId)
                    .Select(
                        s =>
                            new
                            {
                                s.OrderId,
                                s.InvoiceId,
                                s.OrderForDate,
                                s.OrderPlacedDate,
                                s.OrderCompletedDate,
                                s.OrderStatus
                            }).AsEnumerable().Select(o => new Order
                            {
                                OrderId = o.OrderId,
                                InvoiceId = o.InvoiceId,
                                OrderForDate = o.OrderForDate,
                                OrderPlacedDate = o.OrderPlacedDate,
                                OrderCompletedDate = o.OrderCompletedDate,
                                OrderStatus = o.OrderStatus
                            }).ToList();
            return res;
        }

        public Order GetOrderByInvoiceId(int invoiceId)
        {
            var res = DbSet.FirstOrDefault(o => o.InvoiceId == invoiceId);
            return res;
        }
    }
}
