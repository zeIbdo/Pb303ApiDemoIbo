using QuickCom.Domain.Entities;
using QuickCom.Persistence.Repositories.Generic;

namespace QuickCom.Persistence.Repositories.Abstraction
{
    public interface IProductRepository : IRepositoryAsync<Product>
    {
    }
}
