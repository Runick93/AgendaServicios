using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ServiceCalendarData.Interfaces;
using ServicesCalendarData;
using ServicesCalendarData.Models;
using System.Data.Common;
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
           
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void DbExist()
        {
            _dbContext.Database.EnsureCreated();
        }

        #region Create
        public async Task CreateUserAsync(User newUser)
        {
            try
            {
                if (_dbContext.Users.Any(c => c.Name == newUser.Name))
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
            return await _dbContext.Users.FirstOrDefaultAsync(r => r.Name == userName);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync(); ;
        }

        public async Task<Address> GetAddressAsync(int id)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Address>> GetUserAddressesAsync(int userId)
        {
            return await _dbContext.Addresses.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        public async Task<Service> GetServiceAsync(int id)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Service>> GetAddressServicesAsync(int addressId)
        {
            return await _dbContext.Services.Where(r => r.AddressId == addressId).ToListAsync();
        }

        public async Task<List<Service>> GetServicesAsync()
        {
            return await _dbContext.Services.ToListAsync();
        }

        public async Task<Quota> GetQuotaAsync(int id)
        {
            return await _dbContext.Quotas.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Quota>> GetServiceQuotasAsync(int serviceId)
        {
            return await _dbContext.Quotas.Where(r => r.ServiceId == serviceId).ToListAsync();
        }

        public async Task<List<Quota>> GetQuotasAsync()
        {
            return await _dbContext.Quotas.ToListAsync();
        }
        #endregion

        #region Update
        public async Task UpdateUserAsync(User updatedUser)
        {
            try
            {                
                if (updatedUser == null)                
                    throw new ArgumentNullException(nameof(updatedUser), "El usuario a actualizar no puede ser nulo.");                

                User foundUser = await _dbContext.Users.SingleOrDefaultAsync(u => u.Name == updatedUser.Name).ConfigureAwait(false);

                if (foundUser == null)                
                    throw new Exception("No se encontró el usuario para modificar"); //definir una excepcion custom.
            
                if (foundUser.Password != updatedUser.Password)                
                    foundUser.Password = updatedUser.Password;                           

                if (_dbContext.ChangeTracker.HasChanges())                
                    await _dbContext.SaveChangesAsync().ConfigureAwait(false);                
            }
            catch (DbException ex)
            {
                throw;
            }
        }



        public async Task UpdateAddressAsync(Address updatedAddress)
        {
            try
            {
                if (updatedAddress == null)
                    throw new ArgumentNullException(nameof(updatedAddress), "El domicilio a actualizar no puede ser nulo.");

                Address foundAddress = await _dbContext.Addresses.FirstOrDefaultAsync(u => u.Id == updatedAddress.Id);

                if (foundAddress == null)
                    throw new Exception("No se encontró el domicilio para modificar"); //definir una excepcion custom.

                if (foundAddress.Name != updatedAddress.Name)                
                    foundAddress.Name = updatedAddress.Name;                

                if (foundAddress.Street != updatedAddress.Street)                
                    foundAddress.Street = updatedAddress.Street;                

                if (foundAddress.Number != updatedAddress.Number)                
                    foundAddress.Number = updatedAddress.Number;                

                if (foundAddress.Floor != updatedAddress.Floor)                
                    foundAddress.Floor = updatedAddress.Floor;                

                if (foundAddress.Department != updatedAddress.Department)                
                    foundAddress.Department = updatedAddress.Department;                

                if (_dbContext.ChangeTracker.HasChanges())
                    await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task UpdateServiceAsync(Service updatedService)
        {
            try
            {
                if (updatedService == null)
                    throw new ArgumentNullException(nameof(updatedService), "El servicio a actualizar no puede ser nulo.");

                Service foundService = await _dbContext.Services.FirstOrDefaultAsync(u => u.Id == updatedService.Id);

                if (foundService == null)
                    throw new Exception("No se encontró el servicio para modificar"); //definir una excepcion custom.

                if (foundService.Name != updatedService.Name)
                    foundService.Name = updatedService.Name;

                if (foundService.Responsible_Name != updatedService.Responsible_Name)
                    foundService.Responsible_Name = updatedService.Responsible_Name;

                if (foundService.Type != updatedService.Type)
                    foundService.Type = updatedService.Type;

                if (foundService.Payment_Frequency != updatedService.Payment_Frequency)
                    foundService.Payment_Frequency = updatedService.Payment_Frequency;

                if (foundService.Annual_Payment != updatedService.Annual_Payment)
                    foundService.Annual_Payment = updatedService.Annual_Payment;

                if (foundService.Client_Number != updatedService.Client_Number)
                    foundService.Client_Number = updatedService.Client_Number;

                if (_dbContext.ChangeTracker.HasChanges())
                    await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task UpdateQuotaAsync(Quota updatedQuota)
        {
            try
            {
                if (updatedQuota == null)
                    throw new ArgumentNullException(nameof(updatedQuota), "La cuota a actualizar no puede ser nula.");

                Quota foundQuota = await _dbContext.Quotas.FirstOrDefaultAsync(u => u.Number == updatedQuota.Number);

                if (foundQuota == null)
                    throw new Exception("No se encontró la cuota para modificar"); //definir una excepcion custom

                if (foundQuota.Amount != updatedQuota.Amount)
                    foundQuota.Amount = updatedQuota.Amount;

                if (foundQuota.Payment_Status != updatedQuota.Payment_Status)
                    foundQuota.Payment_Status = updatedQuota.Payment_Status;

                if (foundQuota.Payed_Date != updatedQuota.Payed_Date)
                    foundQuota.Payed_Date = updatedQuota.Payed_Date;

                if (foundQuota.Expiration_Date != updatedQuota.Expiration_Date)
                    foundQuota.Expiration_Date = updatedQuota.Expiration_Date;

                if (foundQuota.Bill_Voucher != updatedQuota.Bill_Voucher)
                    foundQuota.Bill_Voucher = updatedQuota.Bill_Voucher;

                if (foundQuota.Payment_Voucher != updatedQuota.Payment_Voucher)
                    foundQuota.Payment_Voucher = updatedQuota.Payment_Voucher;

                if (_dbContext.ChangeTracker.HasChanges())
                    await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }
        #endregion

        #region Delete
        public async Task DeleteUserAsync(User deleteUser)
        {
            try 
            {
                if (deleteUser == null)                
                    throw new ArgumentNullException(nameof(deleteUser), "El objeto a eliminar no puede ser nulo.");
                
                var foundUser = await _dbContext.Users.SingleOrDefaultAsync(e => e.Name == deleteUser.Name).ConfigureAwait(false);

                if (foundUser == null)                
                    throw new Exception("No se encontro el objeto a eliminar");
                
                _dbContext.Users.Remove(foundUser);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task DeleteAddressAsync(Address deleteAddress)
        {
            try 
            {
                if (deleteAddress == null)
                    throw new ArgumentNullException(nameof(deleteAddress), "El objeto a eliminar no puede ser nulo.");

                var foundAddress = await _dbContext.Addresses.SingleOrDefaultAsync(e => e.Id == deleteAddress.Id).ConfigureAwait(false);

                if (foundAddress == null)                
                    throw new Exception("No se encontro el objeto a eliminar");
                
                _dbContext.Addresses.Remove(foundAddress);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task DeleteServiceAsync(Service deleteService)
        {
            try
            {
                if (deleteService == null)
                    throw new ArgumentNullException(nameof(deleteService), "El objeto a eliminar no puede ser nulo.");

                var serviceToDelete = await _dbContext.Services.SingleOrDefaultAsync(e => e.Id == deleteService.Id);

                if (serviceToDelete == null)
                    throw new Exception("No se encontro el objeto a eliminar");

                _dbContext.Services.Remove(serviceToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }

        public async Task DeleteQuotaAsync(Quota deleteQuota)
        {
            try
            {
                if (deleteQuota == null)
                    throw new ArgumentNullException(nameof(deleteQuota), "El objeto a eliminar no puede ser nulo.");

                var quotaToDelete = await _dbContext.Quotas.SingleOrDefaultAsync(e => e.Id == deleteQuota.Id);

                if (quotaToDelete == null)
                    throw new Exception("No se encontro el objeto a eliminar");

                _dbContext.Quotas.Remove(quotaToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw; // Re-lanza la excepción después de manejarla
            }
        }
        #endregion
    }
}
