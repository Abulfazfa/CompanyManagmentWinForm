using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3;
using WindowsFormsApp3.Models;

namespace Business.Interfaces
{
    public interface IEmployee
    {
        bool Create(WindowsFormsApp3.Models.Employee employee);
        bool Update(string name, WindowsFormsApp3.Models.Employee employee);
        bool Delete(string name);
        WindowsFormsApp3.Models.Employee GetById(int id);
        List<WindowsFormsApp3.Models.Employee> GetAllByAge(int age);
        List<WindowsFormsApp3.Models.Employee> GetAllByDepartmentId(int departmentId);
        List<WindowsFormsApp3.Models.Employee> GetAll(Predicate<WindowsFormsApp3.Models.Employee>predicate);
    }
}
