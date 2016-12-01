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
    public class OrderStatusRepository : EFRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public OrderStatus GetNextLogicalStatusFor(int logicalOrder)
        {
            var res = DbSet.FirstOrDefault(o => o.LogicalOrder == logicalOrder + 1);
            return res;
        }

        public OrderStatus GetFirstLogicalStatus()
        {
            var res = DbSet.FirstOrDefault(o => o.LogicalOrder == (int)OrderStatus.StatusOrder.AwaitingPayment);
            return res;
        }
    }
}