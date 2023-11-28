using DataAccess;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.Repositories
{
    public class EmployeeRepository
    {
        private readonly DBContext dbContext;

        public EmployeeRepository()
        {
            dbContext = new DBContext();
        }

        public bool Create(Employee obj)
        {
            try
            {
                string query = $"INSERT INTO Employees (Name, Surname, Address, Age, DepartmentId) " +
                               $"VALUES ('{obj.Name}', '{obj.Surname}', '{obj.Address}', {obj.Age}, {obj.DepartmentId})";
                int rowsAffected = dbContext.ExecuteNonQuery(query);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }

        public bool Delete(Employee obj)
        {
            try
            {
                string query = $"DELETE FROM Employees WHERE EmployeeId = {obj.Id}";
                int rowsAffected = dbContext.ExecuteNonQuery(query);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
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
                // Handle exception or log the error
                throw ex;
            }
        }

        public bool Update(Employee obj)
        {
            try
            {
                string query = $"UPDATE Employees SET Name = '{obj.Name}', Surname = '{obj.Surname}', " +
                               $"Address = '{obj.Address}', Age = {obj.Age}, DepartmentId = {obj.DepartmentId} " +
                               $"WHERE EmployeeId = {obj.Id}";

                int rowsAffected = dbContext.ExecuteNonQuery(query);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }

        public List<Employee> GetAll(Predicate<Employee> predicate = null)
        {
            try
            {
                string query = "SELECT * FROM Employees";
                DataTable result = dbContext.ExecuteQuery(query);
                List<Employee> employees = new List<Employee>();

                foreach (DataRow row in result.Rows)
                {
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(row["EmployeeId"]),
                        Name = row["Name"].ToString(),
                        Surname = row["Surname"].ToString(),
                        Address = row["Address"].ToString(),
                        Age = Convert.ToInt32(row["Age"]),
                        DepartmentId = Convert.ToInt32(row["DepartmentId"])
                    };
                    employees.Add(employee);
                }
                return employees;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }
    }
}
