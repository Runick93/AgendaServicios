using ServicesCalendarCore.Database;
using ServicesCalendarCore.Managers;

namespace AppTester
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var context = new DatabaseManager();
            var users = context.GetUsers().Result;
        }        
    }
}
