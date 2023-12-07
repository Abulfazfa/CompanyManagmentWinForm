using Business.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccess.Repositories;
using WindowsFormsApp3.Models;

namespace Business.Services
{
    public class DepartmentService : IDepartment
    {
        public static int Id { get; set; }
        DepartmentRepository departmentReposity { get; set; }

        public DepartmentService()
        {
            departmentReposity = new DepartmentRepository();
        }

        public bool Create(Department department)
        {
            try
            {
                if (departmentReposity.Get(dep => dep.Name == department.Name) == null)
                {
                    departmentReposity.Create(department);
                    return true;
                }
                return false;              
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool Update(int id, Department department)
        {
            try
            {
                Department filtereddepartment = departmentReposity.Get(dep => dep.Id == id);
                if (department.Name != filtereddepartment.Name)
                {
                    Department isAvaible = departmentReposity.Get(dep => dep.Name == department.Name);
                    if (isAvaible == null)
                    {
                        if (department.Name != null)
                        {
                            filtereddepartment.Name = department.Name;
                        }
                        if (department.Capacity != 0)
                        {
                            filtereddepartment.Capacity = department.Capacity;
                        }

                        departmentReposity.Update(filtereddepartment);
                        return true;

                    }
                    return false;
                }
                else
                {
                    if (department.Capacity != 0)
                    {
                        filtereddepartment.Capacity = department.Capacity;
                    }

                    departmentReposity.Update(filtereddepartment);
                    return true;
                }
               
                   
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(int id)
        {
            if (departmentReposity.Get(dep => dep.Id == id) != null)
            {
                departmentReposity.Delete(GetById(id));
                return true;
            }
            return false;
        }

        public Department GetById(int id)
        {
            if (departmentReposity.Get(dep => dep.Id == id) != null)
            {               
                return departmentReposity.Get(dep => dep.Id == id);
            }
            return null;
        }
        public Department Get(Predicate<Department> predicate)
        {
            return departmentReposity.Get(predicate);
        }

        public List<Department> GetAll()
        {
            return departmentReposity.GetAll();
        }
        public List<Department> SearchMethodforDepartments(Func<Department, bool> predicate)
        {
            return departmentReposity.GetAll(predicate);
        }
    }
}
