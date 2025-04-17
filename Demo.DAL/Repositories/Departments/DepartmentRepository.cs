using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Data;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {        
        
        }
        
    }
}
