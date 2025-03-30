using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data;
using Demo.DAL.Entities;
using Demo.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<T> GetAll(bool withNoTraking = true)
        {
            if (withNoTraking)
                return _dbContext.Set<T>().AsNoTracking().ToList();
            return _dbContext.Set<T>().ToList();
        }
        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public T? Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            Console.WriteLine($"Updating entity: {entity.Id}");
            Console.WriteLine($"New Description: {(entity is Department d ? d.Description : "N/A")}");

            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
