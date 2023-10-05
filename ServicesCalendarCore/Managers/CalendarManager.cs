using ServiceCalendarData.Interfaces;
using ServicesCalendarData;
using ServicesCalendarData.Managers;
using ServicesCalendarData.Models;

namespace ServicesCalendarCore.Managers
{
    public class CalendarManager : ICalendarManager
    {
        private readonly DatabaseManager _DataBaseManager;

        public CalendarManager()
        {
            _DataBaseManager = new DatabaseManager(); 
        }

        public async Task ExecuteProgram()
        {
            try
            {
                //Inserts
                //User newUser = new()
                //{
                //    Name = Guid.NewGuid().ToString(),
                //    Password = Guid.NewGuid().ToString(),
                //};

                //await _DataBaseManager.CreateUserAsync(newUser);
                //Address newAddress = new()
                //{
                //    UserId = 1 // /esto deberia parametrizarse respecto al id del usuario.
                //};
                //await _DataBaseManager.CreateAddressAsync(newAddress);


                //Service newService = new()
                //{
                //    Type = "",
                //    AddressId = 1, //esto deberia parametrizarse respecto al id del domicilio.
                //    Name = "Edesur",
                //    Payment_Frequency = "bimentral"
                //};
                //await _DataBaseManager.CreateServiceAsync(newService);


                //Selects
                var user = await _DataBaseManager.GetUserAsync("Runick");
                var users = await _DataBaseManager.GetUsersAsync();

                var address = await _DataBaseManager.GetAddressAsync(1);
                var userAddresses = await _DataBaseManager.GetUserAddressesAsync(1);
                //var addresses = await _DataBaseManager.GetUserAsync();

                var service = await _DataBaseManager.GetServiceAsync(1);
                var addressServices = await _DataBaseManager.GetAddressServicesAsync(1);
                //var services = await _DataBaseManager.GetServicesAsync();

                var quota = await _DataBaseManager.GetQuotaAsync(23);
                var serviceQuotas = await _DataBaseManager.GetServiceQuotasAsync(8);
                //var quotas = await _DataBaseManager.GetQuotasAsync();


                //Updates

                //Deletes
            }
            catch (Exception ex) { throw; }
        }
    }
}
