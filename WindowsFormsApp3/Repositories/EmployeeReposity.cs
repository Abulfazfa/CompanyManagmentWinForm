using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WindowsFormsApp3;
using WindowsFormsApp3.Models;

namespace DataAccess.Repositories
{
    public class EmployeeRepository
    {
        private readonly SpotifyEntities1 dbContext;

        public EmployeeRepository()
        {
            dbContext = new SpotifyEntities1();
        }

        public bool Create(Employee obj)
        {
            try
            {
                dbContext.Employees.Add(obj);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Employee obj)
        {
            try
            {
                dbContext.Employees.Remove(obj);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employee Get(Predicate<Employee> predicate)
        {
            try
            {
                List<Employee> employees = GetAll();
                return employees.Find(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Employee obj)
        {
            try
            {
                var existEmployee = dbContext.Employees.FirstOrDefault(e => e.EmployeeId == obj.EmployeeId);
                existEmployee = obj;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Employee> GetAll(Func<Employee, bool> predicate = null)
        {
            try
            {
                if (predicate != null)
                {
                    return dbContext.Employees.Where(predicate).ToList();
                }
                else
                {
                    return dbContext.Employees.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
