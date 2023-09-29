namespace ServicesCalendarCore.Models
{
    public  class Quota
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int Number { get; set; }
        public string Month { get; set; } = "";
        public int Amount { get; set; }
        public int PaymentStatus { get; set; }
        public string PayedDate { get; set; } = "";
        public string ExpirationDate { get; set; } = "";
        public Byte[] BillVoucher { get; set; }
        public Byte[] PaymentVoucher { get; set; }
    }
}
