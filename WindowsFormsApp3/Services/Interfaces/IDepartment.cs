using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Models;

namespace Business.Interfaces
{
    public interface IDepartment
    {
        bool Create(Department department);
        bool Update(string name, Department department);
        bool Delete(string name);
        Department GetById(int id);
        List<Department> GetAll();
    }
}
