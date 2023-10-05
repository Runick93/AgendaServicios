using System.ComponentModel.DataAnnotations;

namespace ServicesCalendarData.Models
{
    public  class Quota
    {
        public int Id { get; set; }
        public int ServiceId { get; set; } public Service Service { get; set; }
        public int Number { get; set; }
        public string? Month { get; set; }
        public int Amount { get; set; }
        public int Payment_Status { get; set; }
        public string? Payed_Date { get; set; }
        public string? Expiration_Date { get; set; }
        public Byte[]? Bill_Voucher { get; set; }
        public Byte[]? Payment_Voucher { get; set; }
    }
}
