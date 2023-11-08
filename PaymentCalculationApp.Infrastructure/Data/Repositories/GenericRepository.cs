
using Microsoft.EntityFrameworkCore;
using PaymentCalculation.Domain.Entities;
using PaymentCalculation.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> DbSet { get; }
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);
            DbSet.Remove(entity);
        }


        public async Task<List<T>> GetAysnc(int? skip, int? take, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            foreach (var include in includes)
                query = query.Include(include);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return await query.ToListAsync();

        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            query = query.Where(entity => entity.Id == id);

            foreach (var include in includes)
                query = query.Include(include);

            return await query.SingleOrDefaultAsync();

        }

        public async Task<List<T>> GetFilteredAysnc(Expression<Func<T, bool>>[] fillters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            foreach (var fillter in fillters)
                query = query.Where(fillter);

            foreach (var include in includes)
                query = query.Include(include);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return await query.ToListAsync();

        }

        public async Task<int> InsertAsync(T entity)
        {
            await DbSet.AddAsync(entity);

            return entity.Id;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public Task<List<T>> GetFilteredAysnc(object fileterObject, int? skip, int? take, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllEmployeesAsync()
        {
            IQueryable<T> query = DbSet;

            return await query.ToListAsync();
        }
    }
}
