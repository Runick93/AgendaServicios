using System.ComponentModel.DataAnnotations;

namespace ServicesCalendarData.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int AddressId { get; set; } public Address Address { get; set; }
        public string? Name { get; set; }
        public string? Responsible_Name { get; set; }
        public string Type { get; set; } 
        public string? Payment_Frequency { get; set; }
        public int Annual_Payment { get; set; }
        public string? Client_Number { get; set; }

        public List<Quota> Quotas { get; set; } 
    }
}
