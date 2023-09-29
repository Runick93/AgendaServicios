using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Dapper;

namespace ServicesCalendarCore.Managers
{
    public class DatabaseManager
    {
        public IDbConnection _dbConnection;

        public DatabaseManager() 
        {
            string connectionString = "Data Source=D:\\Temp\\testDataBase.db;Version=3;";
            _dbConnection = new SQLiteConnection(connectionString);
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
        public async Task GetUsers()
        {

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
