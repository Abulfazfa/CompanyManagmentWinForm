using DataAccess;
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
        //private readonly Entities dbContext = new Entities();


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

        public User Get(Predicate<User> predicate)
        {
            try
            {
                List<User> appUsers = GetAll();
                return appUsers.Find(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetAll(Func<User, bool> predicate = null)
        {
            try
            {
                SpotifyEntities1 Entities = new SpotifyEntities1();
                if (predicate != null)
                {
                    return Entities.Users.Where(predicate).ToList();
                }
                else
                {
                    return Entities.Users.ToList();
                }
            }
            catch (Exception ex)
            {
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
