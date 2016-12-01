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
    public class QuestionRepository : EFRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
