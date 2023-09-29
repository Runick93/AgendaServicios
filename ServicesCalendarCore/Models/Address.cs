namespace ServicesCalendarCore.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }= "";
        public string Street { get; set; } = "";
        public int Number { get; set; }
        public int Floor { get; set; }
        public string Departament { get; set; } = "";
    }
}
