using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data;
using Demo.DAL.Repositories.Departments;
using Demo.DAL.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_dbContext);
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();    
        }

        public async ValueTask DisposeAsync()
        {
           await _dbContext.DisposeAsync();
        }
    }
}
