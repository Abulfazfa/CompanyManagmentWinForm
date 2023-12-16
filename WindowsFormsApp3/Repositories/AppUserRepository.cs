using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Models;

namespace WindowsFormsApp3.Repositories
{
    public class AppUserRepository
    {
        private readonly SpotifyEntities1 dbContext = new SpotifyEntities1();

        public bool Create(User obj)
        {
            try
            {
                dbContext.Users.Add(obj);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(User obj)
        {
            try
            {
                dbContext.Users.Remove(obj);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                if (predicate != null)
                {
                    return dbContext.Users.Where(predicate).ToList();
                }
                else
                {
                    return dbContext.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(User obj)
        {
            try
            {
                var existUser = dbContext.Users.FirstOrDefault(e => e.Id == obj.Id);
                existUser = obj;
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
