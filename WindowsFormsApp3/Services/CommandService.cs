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
    public class CommandService
    {
        private readonly CommandRepository commandRepository;

        public CommandService()
        {
            commandRepository = new CommandRepository();
        }

        public Command GetByName(string name)
        {
            return commandRepository.Get(emp => emp.Username == name);
        }
        public List<Command> GetAll()
        {
            return commandRepository.GetAll();
        }
        public List<Command> SearchMethodforCommands(Func<Command, bool> predicate)
        {
            return commandRepository.GetAll(predicate);
        }
        public bool Create(Command command)
        {
            try
            {
                commandRepository.Create(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
