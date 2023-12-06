using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
