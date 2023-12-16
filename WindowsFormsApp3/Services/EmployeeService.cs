﻿using Business.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;
using WindowsFormsApp3.Models;

namespace Business.Services
{
    public class EmployeeService
    {

        private readonly EmployeeRepository employeeRepository;
        private readonly DepartmentRepository departmentReposity;
        public EmployeeService()
        {
            employeeRepository = new EmployeeRepository();
            departmentReposity = new DepartmentRepository();
        }
        public bool Create(WindowsFormsApp3.Models.Employee employee)
        {
            try
            {
                if (employeeRepository.Get(emp => emp.Name.ToLower() == employee.Name.ToLower()) == null)
                {
                    WindowsFormsApp3.Models.Department filtered = departmentReposity.Get(dep => dep.Id == employee.DepartmentId);
                    if (filtered != null)
                    {
                        filtered.MemberCount++;
                        departmentReposity.Update(filtered);
                    }
                    else
                    {
                        MessageBox.Show("There is no such department ");
                        return false;
                    }

                    if (filtered.MemberCount <= filtered.Capacity)
                    {
                        employee.CreatingTime = DateTime.Now;
                        return employeeRepository.Create(employee);
                    }
                    else
                    {
                        MessageBox.Show("You exceeded the capacity limit");
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("There is already a employee with that name and surname");
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                WindowsFormsApp3.Models.Employee deletedEmployee = employeeRepository.Get(emp => emp.EmployeeId == id);
                WindowsFormsApp3.Models.Department department = departmentReposity.Get(dep => dep.Id == deletedEmployee.DepartmentId);
                if (deletedEmployee != null)
                {
                    department.MemberCount--;
                    departmentReposity.Update(department);
                    return employeeRepository.Delete(deletedEmployee);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WindowsFormsApp3.Models.Employee> GetAll(Func<WindowsFormsApp3.Models.Employee,bool> predicate = null)
        {
            return employeeRepository.GetAll(predicate);
        }
        public List<WindowsFormsApp3.Models.Employee> GetAllByAge(int age)
        {
            return employeeRepository.GetAll(emp => emp.Age == age);
        }
        public WindowsFormsApp3.Models.Employee GetById(int id)
        {
            return employeeRepository.Get(emp => emp.EmployeeId == id);
        }
        public WindowsFormsApp3.Models.Employee GetByName(string name)
        {
            return employeeRepository.Get(emp => emp.Name == name);
        }
        public List<WindowsFormsApp3.Models.Employee> GetAllByDepartmentId(int id)
        {
            return employeeRepository.GetAll(emp => emp.DepartmentId == id);
        }
        public List<WindowsFormsApp3.Models.Employee> SearchMethodforEmployeesByNameOrSurname(Func<WindowsFormsApp3.Models.Employee, bool> predicate)
        {
            return employeeRepository.GetAll(predicate);
        }

        public bool Update(int id, WindowsFormsApp3.Models.Employee employee)
        {
            try
            {
                WindowsFormsApp3.Models.Employee filtered = employeeRepository.Get(emp => emp.EmployeeId == id);
                WindowsFormsApp3.Models.Department department = departmentReposity.Get(dep => dep.Id == filtered.DepartmentId);

                if (filtered.DepartmentId != employee.DepartmentId)
                {
                    WindowsFormsApp3.Models.Department department1 = departmentReposity.Get(dep => dep.Id == employee.DepartmentId);
                    if (department1 != null)
                    {
                        if (employee.Name != null)
                        {
                            filtered.Name = employee.Name;
                        }
                        if (employee.Surname != null)
                        {
                            filtered.Surname = employee.Surname;
                        }
                        if (employee.Age != 0)
                        {
                            filtered.Age = employee.Age;
                        }
                        if (employee.Address != null)
                        {
                            filtered.Address = employee.Address;
                        }
                        if (employee.DepartmentId != 0)
                        {
                            if (department1.MemberCount + 1 <= department1.Capacity)
                            {
                                department.MemberCount--;
                                department1.MemberCount++;

                                filtered.DepartmentId = employee.DepartmentId;
                            }
                            else
                            {
                                MessageBox.Show("There isn't enough place in this department");
                                return false;
                            }

                        }
                        employeeRepository.Update(filtered);
                        return true;

                    }
                    return false;

                }
                else
                {
                    if (employee.Name != null)
                    {
                        filtered.Name = employee.Name;
                    }
                    if (employee.Surname != null)
                    {
                        filtered.Surname = employee.Surname;
                    }
                    if (employee.Age != 0)
                    {
                        filtered.Age = employee.Age;
                    }
                    if (employee.Address != null)
                    {
                        filtered.Address = employee.Address;
                    }
                    employeeRepository.Update(filtered);
                    return true;
                }





            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}
