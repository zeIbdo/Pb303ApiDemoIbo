using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QuickCom.Domain.Entities;

namespace QuickCom.Persistence.Repositories.Generic
{

    public class EfRepository<TEntity, TContext> : IRepositoryAsync<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {
        protected TContext Context { get; }

        public EfRepository(TContext context)
        {
            Context = context;
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }


        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (include != null) query = include(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                           int index = 0, int size = 10, bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Context.Set<TEntity>();

            if (!enableTracking) queryable = queryable.AsNoTracking();

            if (include != null) queryable = include(queryable);

            if (predicate != null) queryable = queryable.Where(predicate);

            if (orderBy != null) queryable = orderBy(queryable);

            return await queryable.ToPaginateAsync(index, size);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            //Context.Entry(entity).State = EntityState.Added;

            var entityEntry = await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            //Context.Entry(entity).State = EntityState.Modified;

            var entityEntry = Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            //Context.Entry(entity).State = EntityState.Deleted;

            var entityEntry = Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
    public static class IQueryablePaginateExtensions
    {
        public static async Task<Paginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size)
        {

            int count = await source.CountAsync();
            List<T> items = await source.Skip(index * size).Take(size).ToListAsync();

            Paginate<T> list = new()
            {
                Index = index,
                Size = size,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };

            return list;
        }
    }
    public class BasePageableDto
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}
