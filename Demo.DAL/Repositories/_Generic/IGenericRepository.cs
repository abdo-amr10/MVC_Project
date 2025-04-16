using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities;
using Demo.DAL.Entities.Departments;

namespace Demo.DAL.Repositories._Generic
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(bool withNoTraking = true);
        IQueryable<T> GetIQueryable();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
