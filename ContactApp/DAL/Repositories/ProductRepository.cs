using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public void DeleteWithImage(int productId)
        {
            var product = DbSet.Find(productId);

            DeleteEntity(product.Image, DbContext.Set<Image>());
        }
    }
}
