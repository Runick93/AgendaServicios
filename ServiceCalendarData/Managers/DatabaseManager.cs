using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ServiceCalendarData.Interfaces;
using ServicesCalendarData;
using ServicesCalendarData.Models;
using System.Linq;

namespace ServicesCalendarData.Managers
{
    public class DatabaseManager: IDisposable, IDatabaseManager
    {
        private readonly ServicesCalendarDbContext _dbContext;
        enum Months
        {
            Enero = 1,
            Febrero = 2,
            Marzo = 3,
            Abril = 4,
            Mayo = 5 ,
            Junio = 6,
            Julio = 7,
            Agosto = 8,
            Septiembre = 9 ,
            Octubre = 10,
            Noviembre = 11,
            Diciembre = 12 
        }

        public DatabaseManager()
        {
            _dbContext = new ServicesCalendarDbContext();
           //_dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        #region Create
        public async Task CreateUserAsync(User newUser)
        {
            try
            {
                if (_dbContext.Users.Any(a => a.Name == newUser.Name))
                    throw new Exception("El usuario ya existe.");

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();
                
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al crear el usuario.", ex);
            }
        }

        public async Task CreateAddressAsync(Address newAddress)
        {
            try
            {
                _dbContext.Addresses.Add(newAddress);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el domicilio.", ex);
            }
        }

        public async Task CreateServiceAsync(Service newService)
        {
            try 
            {
                _dbContext.Services.Add(newService);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el servicio.", ex);
            }

            var quotaMax = newService.Payment_Frequency switch
            {
                "mensual" => 12,
                "bimentral" => 6,
                "trimestral" => 4,
                _ => 12,
            };

            for (int quotaNumber = 1; quotaNumber <= quotaMax; quotaNumber++)
            {
                Months month = (Months)quotaNumber;
                Quota quota = new()
                {
                    ServiceId = newService.Id,
                    Number = quotaNumber,
                    Month = month.ToString()
                };
                await CreateQuotaAsync(quota);
            }
        }

        public async Task CreateQuotaAsync(Quota newQuota)
        {
            try 
            { 
                _dbContext.Quotas.Add(newQuota);
                await _dbContext.SaveChangesAsync();
            }   
            catch (Exception ex)
            {
                throw new Exception("Error al crear la cuota.", ex);
            }
        }


        #endregion

        #region Read
        public async Task<User> GetUserAsync(string userName)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == userName);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync(); ;
        }


        public async Task<Address> GetAddressAsync(int id)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Address>> GetUserAddressesAsync(int userId)
        {
            return await _dbContext.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            return await _dbContext.Addresses.ToListAsync();
        }


        public async Task<Service> GetServiceAsync(int id)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Service>> GetAddressServicesAsync(int addressId)
        {
            return await _dbContext.Services.Where(a => a.AddressId == addressId).ToListAsync();
        }

        public async Task<List<Service>> GetServicesAsync()
        {
            return await _dbContext.Services.ToListAsync();
        }


        public async Task<Quota> GetQuotaAsync(int id)
        {
            return await _dbContext.Quotas.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Quota>> GetServiceQuotasAsync(int serviceId)
        {
            return await _dbContext.Quotas.Where(a => a.ServiceId == serviceId).ToListAsync();
        }

        public async Task<List<Quota>> GetQuotasAsync()
        {
            return await _dbContext.Quotas.ToListAsync();
        }
        #endregion

        #region Update
        public async Task UpdateUserAsync(User userUpdate)
        {
            try
            { 
                User userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == userUpdate.Name);
                if (userToUpdate != null)
                {
                    if(userToUpdate.Password != userUpdate.Password)
                        userToUpdate.Password = userUpdate.Password;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAddressAsync(Address addressUpdate)
        {
            try
            { 
                Address addressToUpdate = await _dbContext.Addresses.FirstOrDefaultAsync(u => u.Id == addressUpdate.Id);
                if (addressToUpdate != null)
                {
                    if (addressToUpdate.Name != addressUpdate.Name)                
                        addressToUpdate.Name = addressUpdate.Name;                

                    if (addressToUpdate.Street != addressUpdate.Street)                
                        addressToUpdate.Street = addressUpdate.Street;                

                    if (addressToUpdate.Number != addressUpdate.Number)                
                        addressToUpdate.Number = addressUpdate.Number;                

                    if (addressToUpdate.Floor != addressUpdate.Floor)                
                        addressToUpdate.Floor = addressUpdate.Floor;                

                    if (addressToUpdate.Department != addressUpdate.Department)                
                        addressToUpdate.Department = addressUpdate.Department;                

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task UpdateServiceAsync(Service serviceUpdate)
        {
            try
            {
                Service serviceToUpdate = await _dbContext.Services.FirstOrDefaultAsync(u => u.Id == serviceUpdate.Id);

                if (serviceToUpdate != null)
                {
                    if (serviceToUpdate.Name != serviceUpdate.Name)
                        serviceToUpdate.Name = serviceUpdate.Name;

                    if (serviceToUpdate.Responsible_Name != serviceUpdate.Responsible_Name)
                        serviceToUpdate.Responsible_Name = serviceUpdate.Responsible_Name;

                    if (serviceToUpdate.Type != serviceUpdate.Type)
                        serviceToUpdate.Type = serviceUpdate.Type;

                    if (serviceToUpdate.Payment_Frequency != serviceUpdate.Payment_Frequency)
                        serviceToUpdate.Payment_Frequency = serviceUpdate.Payment_Frequency;

                    if (serviceToUpdate.Annual_Payment != serviceUpdate.Annual_Payment)
                        serviceToUpdate.Annual_Payment = serviceUpdate.Annual_Payment;

                    if (serviceToUpdate.Client_Number != serviceUpdate.Client_Number)
                        serviceToUpdate.Client_Number = serviceUpdate.Client_Number;

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task UpdateQuotaAsync(Quota quotaUpdate)
        {
            try
            {
                Quota quotaToUpdate = await _dbContext.Quotas.FirstOrDefaultAsync(u => u.Number == quotaUpdate.Number);

                if (quotaToUpdate != null)
                {
                    if (quotaToUpdate.Amount != quotaUpdate.Amount)
                        quotaToUpdate.Amount = quotaUpdate.Amount;

                    if (quotaToUpdate.Payment_Status != quotaUpdate.Payment_Status)
                        quotaToUpdate.Payment_Status = quotaUpdate.Payment_Status;

                    if (quotaToUpdate.Payed_Date != quotaUpdate.Payed_Date)
                        quotaToUpdate.Payed_Date = quotaUpdate.Payed_Date;

                    if (quotaToUpdate.Expiration_Date != quotaUpdate.Expiration_Date)
                        quotaToUpdate.Expiration_Date = quotaUpdate.Expiration_Date;

                    if (quotaToUpdate.Bill_Voucher != quotaUpdate.Bill_Voucher)
                        quotaToUpdate.Bill_Voucher = quotaUpdate.Bill_Voucher;

                    if (quotaToUpdate.Payment_Voucher != quotaUpdate.Payment_Voucher)
                        quotaToUpdate.Payment_Voucher = quotaUpdate.Payment_Voucher;

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }
        #endregion

        #region Delete
        public async Task DeleteUserAsync(User UserDelete)
        {
            try 
            { 
                var userToDelete = _dbContext.Users.FirstOrDefault(e => e.Name == UserDelete.Name);

                if (userToDelete != null)
                {
                    _dbContext.Users.Remove(userToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task DeleteAddressAsync(Address addressDelete)
        {
            try 
            { 
                var addressToDelete = _dbContext.Addresses.FirstOrDefault(e => e.Name == addressDelete.Name);

                if (addressToDelete != null)
                {
                    _dbContext.Addresses.Remove(addressToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task DeleteServiceAsync(Service serviceDelete)
        {
            try
            { 
                var serviceToDelete = _dbContext.Services.FirstOrDefault(e => e.Name == serviceDelete.Name);

                if (serviceToDelete != null)
                {
                    _dbContext.Services.Remove(serviceToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task DeleteQuotaAsync(Quota quotaDelete)
        {
            try
            { 
                var quotaToDelete = _dbContext.Quotas.FirstOrDefault(e => e.Id == quotaDelete.Id);

                if (quotaToDelete != null)
                {
                    _dbContext.Quotas.Remove(quotaToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }
        #endregion
    }
}
