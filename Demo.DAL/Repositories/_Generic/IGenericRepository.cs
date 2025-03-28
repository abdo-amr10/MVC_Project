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
        T? Get(int id);
        IEnumerable<T> GetAll(bool withNoTraking = true);
        IQueryable<T> GetAllAsQueryable();
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
