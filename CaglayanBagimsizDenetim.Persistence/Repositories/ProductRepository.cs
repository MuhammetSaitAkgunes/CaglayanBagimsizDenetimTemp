using CaglayanBagimsizDenetim.Application.Interfaces.Repositories;
using CaglayanBagimsizDenetim.Domain.Entities;
using CaglayanBagimsizDenetim.Persistence.Contexts;

namespace CaglayanBagimsizDenetim.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        // base(dbContext) diyerek GenericRepository'nin constructor'ını besliyoruz.
    }
}
