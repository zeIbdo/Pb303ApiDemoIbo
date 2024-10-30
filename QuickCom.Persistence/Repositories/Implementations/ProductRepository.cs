using QuickCom.Domain.Entities;
using QuickCom.Persistence.Context;
using QuickCom.Persistence.Repositories.Abstraction;
using QuickCom.Persistence.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCom.Persistence.Repositories.Implementations
{
    public class ProductRepository : EfRepository<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
