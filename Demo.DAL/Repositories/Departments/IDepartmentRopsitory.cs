using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;

namespace Demo.DAL.Repositories.Departments
{
    public interface IDepartmentRopsitory
    {
        Department? Get(int id);
        IEnumerable<Department> GetAll(bool withNoTraking = true);
        int Add (Department entity);
        int Update (Department entity);
        int Delete (Department entity);

    }
}
