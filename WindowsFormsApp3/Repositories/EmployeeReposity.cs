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

        public bool Create(WindowsFormsApp3.Models.Employee obj)
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

        public bool Delete(WindowsFormsApp3.Models.Employee obj)
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

        public WindowsFormsApp3.Models.Employee Get(Predicate<WindowsFormsApp3.Models.Employee> predicate)
        {
            try
            {
                List<WindowsFormsApp3.Models.Employee> employees = GetAll();
                return employees.Find(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(WindowsFormsApp3.Models.Employee obj)
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

        public List<WindowsFormsApp3.Models.Employee> GetAll(Func<WindowsFormsApp3.Models.Employee, bool> predicate = null)
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
