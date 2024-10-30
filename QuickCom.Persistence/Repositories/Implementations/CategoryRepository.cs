using QuickCom.Domain.Entities;
using QuickCom.Persistence.Context;
using QuickCom.Persistence.Repositories.Abstraction;
using QuickCom.Persistence.Repositories.Generic;

namespace QuickCom.Persistence.Repositories.Implementations
{
    public class CategoryRepository : EfRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
