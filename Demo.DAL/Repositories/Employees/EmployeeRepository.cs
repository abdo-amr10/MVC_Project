using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Repositories._Generic;

namespace Demo.DAL.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
