using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext dbContext;
    private DbSet<TEntity> entity => dbContext.Set<TEntity>();

    public GenericRepository(DbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await this.entity.AddAsync(entity);
        return await dbContext.SaveChangesAsync();
    }

    public virtual int Add(TEntity entity)
    {
        this.entity.Add(entity);
        return dbContext.SaveChanges();
    }

    public virtual int Add(IEnumerable<TEntity>? entities)
    {
        if (entities != null && !entities.Any())
        {
            return 0;
        }

        entity.AddRange(entities);
        return dbContext.SaveChanges();
    }

    public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
        {
            return 0;
        }

        await entity.AddRangeAsync(entities);
        return await dbContext.SaveChangesAsync();
    }

    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return await dbContext.SaveChangesAsync();
    }

    public virtual int Update(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
        return dbContext.SaveChanges();
    }

    public virtual async Task<int> DeleteAsync(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);
        return await dbContext.SaveChangesAsync();
    }

    public virtual int Delete(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);
        return dbContext.SaveChanges();
    }

    public virtual async Task<int> DeleteAsync(Guid id)
    {
        var entity = await this.entity.FindAsync(id);
        return await DeleteAsync(entity);
    }

    public virtual int Delete(Guid id)
    {
        var entity = this.entity.Find(id);
        return Delete(entity);
    }

    public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return dbContext.SaveChanges() > 0;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return await dbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
    {
        if (!this.entity.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
        {
            dbContext.Update(entity);
        }
        return await dbContext.SaveChangesAsync();
    }

    public virtual int AddOrUpdate(TEntity entity)
    {
        if (!this.entity.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
        {
            dbContext.Update(entity);
        }
        return dbContext.SaveChanges();
    }

    public virtual IQueryable<TEntity> AsQueryAble() => entity.AsQueryable();

    public virtual async Task<List<TEntity>> GetAllAsync(bool noTracking = true)
    {
        if (noTracking)
        {
            return await entity.AsNoTracking().ToListAsync();
        }

        return await entity.ToListAsync();
    }


    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] inlcudes)
    {
        IQueryable<TEntity> query = entity;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        foreach (Expression<Func<TEntity, object>> include in inlcudes)
        {
            query = query.Include(include);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        TEntity found = await entity.FindAsync(id);

        if (found == null)
            return null;

        if (noTracking)
            dbContext.Entry(found).State = EntityState.Detached;

        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            dbContext.Entry(found).Reference(include).Load();
        }

        return found;
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync();
    }

    public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return Get(predicate, noTracking, includes).FirstOrDefaultAsync();
    }

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = entity.AsQueryable();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual Task BulkDeleteByIdAsync(IEnumerable<Guid> ids)
    {
        if (ids != null && !ids.Any())
        {
            return Task.CompletedTask;
        }

        dbContext.RemoveRange(entity.Where(x => ids.Contains(x.Id)));
        return dbContext.SaveChangesAsync();
    }

    public virtual Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return dbContext.SaveChangesAsync();
    }

    public virtual Task BulkDeleteAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;

        entity.RemoveRange(entities);
        return dbContext.SaveChangesAsync();
    }

    public virtual Task BulkUpdateAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;

        foreach (var entityItem in entities)
        {
            entity.Update(entityItem);
        }

        return dbContext.SaveChangesAsync();
    }

    public virtual Task BulkAddAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
        {
            return Task.CompletedTask;
        }

        foreach (var entityItem in entities)
        {
            entity.Add(entityItem);
        }

        return dbContext.SaveChangesAsync();
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }
}