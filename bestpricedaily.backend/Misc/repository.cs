using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using bestpricedaily.Models;

namespace bestpricedaily.Misc.Repository
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
    public class BaseEntity : IEntity
    {

        public Guid Id { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }

    }
    public interface IAsyncRepository<T> where T : BaseEntity
    {

        Task<T> GetById(Guid id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);

    }
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {

        #region Fields

        protected readonly DataDbContext _ctx;
        //private DbSet<T> entities;
        string errMsg = string.Empty;

        #endregion

        public EfRepository(DataDbContext context)
        {
            _ctx = context;
            //entities = context.Set<T>();
        }

        #region Public Methods

        public Task<T> GetById(Guid id) => _ctx.Set<T>().FindAsync(id).AsTask();

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) {
            var x= _ctx.Set<T>().FirstOrDefaultAsync(predicate) ; 
            return x;
        }

        public async Task Add(T entity)
        {
            // await Context.AddAsync(entity);
            await _ctx.Set<T>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            // In case AsNoTracking is used
            _ctx.Entry(entity).State = EntityState.Modified;
            return _ctx.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            return _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _ctx.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => _ctx.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => _ctx.Set<T>().CountAsync(predicate);

        #endregion

    }
    public class DataDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)        { }
    }


}
