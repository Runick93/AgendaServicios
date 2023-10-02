using ServicesCalendarCore.Database;
using ServicesCalendarCore.Managers;

namespace AppTester
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var manager = new DatabaseManager();


                //Selects
                //var user = manager.GetUser("Runick").Result;
                //var users = manager.GetUsers().Result;

                //var address = manager.GetAddress(1).Result;
                //var addresses = manager.GetAddresses().Result;

                //var service = manager.GetService(1).Result;
                //var services = manager.GetServices().Result;

                //var quota = manager.GetQuota(1).Result;
                //var quotas = manager.GetQuotas().Result;

                //Inserts
                //var users = manager.GetUsers();
                manager.CreateUser("Yamileh", "Passw0rd");
                //users = manager.GetUsers();
            }
            catch (Exception ex) { throw; }
        }        
    }
}
