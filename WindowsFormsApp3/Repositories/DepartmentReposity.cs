using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WindowsFormsApp3.Models;

namespace DataAccess.Repositories
{
    public class DepartmentRepository
    {
        private readonly SpotifyEntities1 dbContext = new SpotifyEntities1();

        
        public bool Create(Department obj)
        {
            try
            {
                dbContext.Departments.Add(obj);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Department obj)
        {
            try
            {
                dbContext.Departments.Remove(obj);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Department Get(Predicate<Department> predicate)
        {
            try
            {
                List<Department> departments = GetAll();
                return departments.Find(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Department> GetAll(Func<Department, bool> predicate = null)
        {
            try
            {
                if (predicate != null)
                {
                    return dbContext.Departments.Where(predicate).ToList();
                }
                else
                {
                    return dbContext.Departments.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Department obj)
        {
            try
            {
                var existDepartment = dbContext.Departments.FirstOrDefault(e => e.Id == obj.Id);
                existDepartment = obj;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
