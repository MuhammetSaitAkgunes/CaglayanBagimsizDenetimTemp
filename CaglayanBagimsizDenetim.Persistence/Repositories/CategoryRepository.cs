using CaglayanBagimsizDenetim.Application.Interfaces.Repositories;
using CaglayanBagimsizDenetim.Domain.Entities;
using CaglayanBagimsizDenetim.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglayanBagimsizDenetim.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
