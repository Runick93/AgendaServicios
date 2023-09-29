using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using ServicesCalendarCore.Database;
using Microsoft.EntityFrameworkCore;
using ServicesCalendarCore.Models;

namespace ServicesCalendarCore.Managers
{
    public class DatabaseManager
    {
        public ServicesCalendarDbContext _dbContext;
        

        public DatabaseManager() 
        {
            _dbContext = new ServicesCalendarDbContext();
        }

        #region Create
        public async Task CreateUser()
        {
            
        }

        public async Task CreateAddress()
        {

        }

        public async Task CreateService()
        {
            //Quizas aca pueda usar un innerjoin con quotas... capaz...  tengo q pensar mejor esto.
            //quizas me convenga usar entity
            for (int i = 0; i <= 12; i++) 
            {
                CreateQuota();
            }
        }

        public async Task CreateQuota()
        {

        }
        #endregion
        #region Read
        public async Task<List<User>> GetUsers()
        {
            List<User> users = _dbContext.Users.ToList();
            return users;   
        }

        public async Task GetAddresses()
        {

        }

        public async Task GetServices()
        {

        }

        public async Task GetQuotas()
        {

        }
        #endregion
        #region Update
        public async Task UpdateUser()
        {

        }

        public async Task UpdateAddress()
        {

        }

        public async Task UpdateService()
        {

        }

        public async Task UpdateQuota()
        {

        }
        #endregion
        #region Delete
        public async Task DeleteUser()
        {

        }

        public async Task DeleteAddress()
        {

        }

        public async Task DeleteService()
        {

        }

        public async Task DeleteQuota()
        {

        }
        #endregion
    }
}
