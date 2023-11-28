using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    public class DepartmentRepository
    {
        private readonly DBContext dbContext;

        public DepartmentRepository()
        {
            dbContext = new DBContext();
        }

        public bool Create(Department obj)
        {
            try
            {
                string query = $"INSERT INTO Departments (Name) VALUES ('{obj.Name}')";
                int rowsAffected = dbContext.ExecuteNonQuery(query);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }

        public bool Delete(Department obj)
        {
            try
            {
                string query = $"DELETE FROM Departments WHERE Id = {obj.Id}";
                int rowsAffected = dbContext.ExecuteNonQuery(query);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
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
                // Handle exception or log the error
                throw ex;
            }
        }

        public List<Department> GetAll(Predicate<Department> predicate = null)
        {
            try
            {
                string query = "SELECT * FROM Departments";
                DataTable table = dbContext.ExecuteQuery(query);
                List<Department> departments = new List<Department>();

                foreach (DataRow row in table.Rows)
                {
                    departments.Add(new Department
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString()
                    });
                }

                return predicate == null ? departments : departments.FindAll(predicate);
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }

        public bool Update(Department obj)
        {
            try
            {
                string query = $"UPDATE Departments SET Name = '{obj.Name}' WHERE Id = {obj.Id}";
                int rowsAffected = dbContext.ExecuteNonQuery(query);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }
    }
}
