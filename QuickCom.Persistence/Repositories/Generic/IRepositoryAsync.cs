using Microsoft.EntityFrameworkCore.Query;
using QuickCom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuickCom.Persistence.Repositories.Generic
{
    public interface IRepositoryAsync<T> where T : Entity
    {
        Task<T?> GetAsync(int id);

        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<Paginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                        int index = 0, int size = 10, bool enableTracking = true);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
