using PaymentCalculation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity //<int>
    {
        Task<List<T>> GetFilteredAysnc(Expression<Func<T, bool>>[] fillters, int? skip, int? take, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetFilteredAysnc(object fileterObject, int? skip, int? take, params Expression<Func<T, object>>[] includes);

        Task<List<T>> GetAysnc(int? skip, int? take, params Expression<Func<T, object>>[] includes);


        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        Task<int> InsertAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync();

        Task<List<T>> GetAllEmployeesAsync();

    }
}
