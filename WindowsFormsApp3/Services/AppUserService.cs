using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using WindowsFormsApp3.Models;
using WindowsFormsApp3.Repositories;

namespace WindowsFormsApp3.Services
{
    public class AppUserService
    {
        private readonly AppUserRepository appUserRepository;

        public AppUserService()
        {
            appUserRepository = new AppUserRepository();
        }

        public User GetByName(string name)
        {
            return appUserRepository.Get(emp => emp.Username == name);
        }
        public User Get(Predicate<User> predicate)
        {
            return appUserRepository.Get(predicate);
        }
        public List<User> GetAll()
        {
            return appUserRepository.GetAll();
        }
        public bool Delete(int id)
        {
            if (appUserRepository.Get(dep => dep.Id == id) != null)
            {
                appUserRepository.Delete(Get(i => i.Id == id));
                return true;
            }
            return false;
        }
        public bool Create(User user)
        {
            try
            {
                if (appUserRepository.Get(dep => dep.Username == user.Username) == null)
                {
                    appUserRepository.Create(user);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool Update(int id, User user)
        {
            try
            {
                User filteredUser = appUserRepository.Get(dep => dep.Id == id);
                if (user.Username != filteredUser.Username)
                {
                    User isAvaible = appUserRepository.Get(dep => dep.Username == user.Username);
                    if (isAvaible == null)
                    {
                        if (user.Username != null)
                        {
                            filteredUser.Username = user.Username;
                        }
                        if (user.Password != null)
                        {
                            filteredUser.Password = user.Password;
                        }

                        appUserRepository.Update(filteredUser);
                        return true;

                    }
                    return false;
                }
                else
                {
                    if (user.Password != null)
                    {
                        filteredUser.Password = user.Password;
                    }

                    appUserRepository.Update(filteredUser);
                    return true;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
