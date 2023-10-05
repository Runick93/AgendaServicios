using ServicesCalendarData.Models;

namespace ServiceCalendarData.Interfaces
{
    public interface IDatabaseManager
    {
        Task CreateUserAsync(User newUser);
        Task CreateAddressAsync(Address newAddress);
        Task CreateServiceAsync(Service newService);
        Task CreateQuotaAsync(Quota newQuota);

        Task<User> GetUserAsync(string userName);
        Task<List<User>> GetUsersAsync();
        Task<Address> GetAddressAsync(int id);
        Task<List<Address>> GetUserAddressesAsync(int userId);
        Task<List<Address>> GetAddressesAsync();
        Task<Service> GetServiceAsync(int id);
        Task<List<Service>> GetAddressServicesAsync(int addressId);
        Task<List<Service>> GetServicesAsync();
        Task<Quota> GetQuotaAsync(int id);
        Task<List<Quota>> GetServiceQuotasAsync(int serviceId);
        Task<List<Quota>> GetQuotasAsync();

        Task UpdateUserAsync(User userUpdate);
        Task UpdateAddressAsync(Address addressUpdate);
        Task UpdateServiceAsync(Service serviceUpdate);
        Task UpdateQuotaAsync(Quota quotaUpdate);

        Task DeleteUserAsync(User UserDelete);
        Task DeleteAddressAsync(Address addressDelete);
        Task DeleteServiceAsync(Service serviceDelete);
        Task DeleteQuotaAsync(Quota quotaDelete);
        void Dispose();
    }
}
