using static TicketCoder.Helper.Enums.Enums;

namespace TicketCoder.Models.Enitites
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TicketNumber {  get; set; }
        public DateTime GeneratedTime { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Qunatity { get; set; }
        public float Price { get; set; }
        public TicketType TicketType { get; set; }
        public virtual Event Event { get; set; }
        public virtual List<UserTickets> UserTickets { get; set; }
    }
}
