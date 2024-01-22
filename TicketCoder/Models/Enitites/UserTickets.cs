namespace TicketCoder.Models.Enitites
{
    public class UserTickets
    {
        public int Id { get; set; }
        public DateTime PurchesDate { get; set; }
        public string TicketSerialNumber { get; set; }
        public virtual User User { get; set; }
        public virtual Ticket Ticket { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateionDate { get; set; }
    }
}
