using Microsoft.EntityFrameworkCore.Diagnostics;
using ServicesCalendarCore.Managers;
using ServicesCalendarData.Managers;

namespace AppTester
{
    public class Program
    {
        private readonly CalendarManager _CalendarManager;

        public Program()
        {
            //_CalendarManager = new CalendarManager();
        }

        public static void Main(string[] args)
        {
            var calendarManager = new CalendarManager();
            calendarManager.ExecuteProgram();
        }        
    }
}
