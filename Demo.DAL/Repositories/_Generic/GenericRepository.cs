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
        public async Task<IEnumerable<T>> GetAllAsync(bool withNoTraking = true)
        {
            if (withNoTraking)
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }
        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Add(T entity)=> _dbContext.Set<T>().Add(entity);

        public void Update(T entity)
        {
            Console.WriteLine($"Updating entity: {entity.Id}");
            Console.WriteLine($"New Description: {(entity is Department d ? d.Description : "N/A")}");

            _dbContext.Set<T>().Update(entity);
            
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
         
        }
    }
}
