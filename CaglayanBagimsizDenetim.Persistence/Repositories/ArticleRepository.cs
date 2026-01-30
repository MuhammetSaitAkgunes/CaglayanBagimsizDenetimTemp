using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaglayanBagimsizDenetim.Application.Interfaces.Repositories;
using CaglayanBagimsizDenetim.Domain.Entities;
using CaglayanBagimsizDenetim.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CaglayanBagimsizDenetim.Persistence.Repositories
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {

        }   
    }
}
