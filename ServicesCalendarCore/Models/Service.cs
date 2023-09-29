namespace ServicesCalendarCore.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string Name { get; set; } = "";
        public string ResponsableName { get; set; } = "";
        public string Type { get; set; } = "";
        public string PaymentFrequency { get; set; } = "";
        public int Annual_Payment { get; set; }
        public string ClientNumber { get; set; } = "";
    }
}
