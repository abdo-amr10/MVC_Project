using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data;
using Demo.DAL.Repositories.Departments;
using Demo.DAL.Repositories.Employees;

namespace Demo.DAL.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;  }
        public IDepartmentRepository DepartmentRepository { get;  }
        Task<int> CompleteAsync();
    }
}
