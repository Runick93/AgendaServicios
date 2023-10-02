using Microsoft.EntityFrameworkCore;
using ServicesCalendarCore.Database;
using ServicesCalendarCore.Models;


namespace ServicesCalendarCore.Managers
{
    public class DatabaseManager
    {
        private readonly ServicesCalendarDbContext _dbContext;


        public DatabaseManager()
        {
            _dbContext = new ServicesCalendarDbContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        #region Create
        public void CreateUser(string name, string password)
        {
            try
            {
                _dbContext.Database.EnsureCreated();
                var newUser = new User
                {
                    Name = name,
                    Password = password
                };

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                
            }
            catch (Exception) 
            {                
                throw;
            }
        }

        public async Task CreateAddress(int currentUserId)
        {
            var newAddress = new Address
            {
                UserId = currentUserId,
                Name = "",
                Street = "",
                Number = 1,
                Floor = 0,
                Department = ""
            };

            _dbContext.Addresses.Add(newAddress);
            _dbContext.SaveChanges();
        }

        public async Task CreateService(int currentAddressId)
        {
            var newService = new Service
            {
                AddressId = currentAddressId,
                Name = "",
                Responsible_Name = "",
                Type = "",
                Payment_Frequency = "",
                Annual_Payment = 0,
                Client_Number = ""

            };

            _dbContext.Services.Add(newService);
            _dbContext.SaveChanges();

            for (int i = 0; i <= 12; i++)
            {
                CreateQuota(i, 3); //el segundo valor representaria el id del servicio  
            }
        }

        public async Task CreateQuota(int quotaNumber, int currentServiceId)
        {
            var newQuota = new Quota
            {
                ServiceId = currentServiceId,
                Number = quotaNumber,
                Month = "",
                Amount = 0,
                Payment_Status = 0,
                Payed_Date = "",
                Expiration_Date = ""

            };

            _dbContext.Quotas.Add(newQuota);
            _dbContext.SaveChanges();
        }


        #endregion
        #region Read
        public async Task<User> GetUser(string nameFind)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Name == nameFind);
        }


        //public async Task<List<User>> GetUsers()
        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }


        public async Task<Address> GetAddress(int id)
        {
            return _dbContext.Addresses.FirstOrDefault(a => a.Id == id);
        }


        public async Task<List<Address>> GetAddresses()
        {
            return _dbContext.Addresses.ToList();
        }


        public async Task<Service> GetService(int id)
        {
            return _dbContext.Services.FirstOrDefault(s => s.Id == id);
        }


        public async Task<List<Service>> GetServices()
        {
            return _dbContext.Services.ToList();
        }


        public async Task<Quota> GetQuota(int id)
        {
            return _dbContext.Quotas.FirstOrDefault(q => q.Id == id);
        }


        public async Task<List<Quota>> GetQuotas()
        {
            return _dbContext.Quotas.ToList();
        }
        #endregion
        #region Update
        public async Task UpdateUser(string name, string newPassword)
        {
            using (var context = new ServicesCalendarDbContext())
            {
                var userToUpdate = context.Users.FirstOrDefault(u => u.Name == name);
                if (userToUpdate != null)
                {
                    userToUpdate.Password = newPassword;
                    context.SaveChanges();
                }
            }
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
