using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data;
using Demo.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Repositories.Departments
{
    internal class DepartmentRepository : IDepartmentRopsitory
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Department> GetAll(bool withNoTraking = true)
        {
            if(withNoTraking)
                return _dbContext.Departments.AsNoTracking().ToList();
            return _dbContext.Departments.ToList();
        }
        public Department? Get(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();   
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }

       
       
        
    }
}
