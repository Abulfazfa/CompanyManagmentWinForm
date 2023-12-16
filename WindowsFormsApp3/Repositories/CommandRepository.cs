using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.DAL;
using WindowsFormsApp3.Models;

namespace WindowsFormsApp3.Repositories
{
    public class CommandRepository
    {
        private readonly SpotifyEntities1 dbContext = new SpotifyEntities1();
        private readonly DBContext dBContext = new DBContext();

        public bool Create(Command command)
        {
            try
            {
                string query = $"INSERT INTO Commands (Username, ApplyDate, UsedFor) VALUES ('{command.Username}', '{command.ApplyDate}', '{command.UsedFor}')";

                int rowsAffected = dBContext.ExecuteNonQuery(query);

                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Command Get(Predicate<Command> predicate)
        {
            try
            {
                List<Command> commands = GetAll();
                return commands.Find(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Command> GetAll(Func<Command, bool> predicate = null)
        {
            try
            {
                if (predicate != null)
                {
                    return dbContext.Commands.Where(predicate).ToList();
                }
                else
                {
                    return dbContext.Commands.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
