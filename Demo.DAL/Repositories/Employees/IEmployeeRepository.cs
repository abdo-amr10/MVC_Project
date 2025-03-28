using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Repositories._Generic;

namespace Demo.DAL.Repositories.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
      
    }
}
