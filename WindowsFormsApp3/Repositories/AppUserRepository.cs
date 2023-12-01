using DataAccess;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Models;

namespace WindowsFormsApp3.Repositories
{
    public class AppUserRepository
    {
        private readonly DBContext dbContext;

        public AppUserRepository()
        {
            dbContext = new DBContext();
        }

        //public bool Create(Department obj)
        //{
        //    try
        //    {
        //        string query = $"INSERT INTO Departments (Name) VALUES ('{obj.Name}')";
        //        int rowsAffected = dbContext.ExecuteNonQuery(query);
        //        return rowsAffected > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception or log the error
        //        throw ex;
        //    }
        //}

        //public bool Delete(Department obj)
        //{
        //    try
        //    {
        //        string query = $"DELETE FROM Departments WHERE Id = {obj.Id}";
        //        int rowsAffected = dbContext.ExecuteNonQuery(query);
        //        return rowsAffected > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception or log the error
        //        throw ex;
        //    }
        //}

        public AppUser Get(Predicate<AppUser> predicate)
        {
            try
            {
                List<AppUser> appUsers = GetAll();
                return appUsers.Find(predicate);
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }

        public List<AppUser> GetAll(Predicate<AppUser> predicate = null)
        {
            try
            {
                string query = "SELECT * FROM Users";
                DataTable table = dbContext.ExecuteQuery(query);
                List<AppUser> appUsers = new List<AppUser>();

                foreach (DataRow row in table.Rows)
                {
                    appUsers.Add(new AppUser
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Username = row["Username"].ToString(),
                        Password = row["Password"].ToString()
                    });
                }

                return predicate == null ? appUsers : appUsers.FindAll(predicate);
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw ex;
            }
        }

        //public bool Update(Department obj)
        //{
        //    try
        //    {
        //        string query = $"UPDATE Departments SET Name = '{obj.Name}' WHERE Id = {obj.Id}";
        //        int rowsAffected = dbContext.ExecuteNonQuery(query);
        //        return rowsAffected > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception or log the error
        //        throw ex;
        //    }
        //}
    }
}
