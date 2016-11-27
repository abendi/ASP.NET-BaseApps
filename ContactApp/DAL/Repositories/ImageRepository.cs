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
    public class ImageRepository : EFRepository<Image>, IImageRepository
    {
        public ImageRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
