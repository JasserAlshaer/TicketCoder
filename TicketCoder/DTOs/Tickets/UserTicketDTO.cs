namespace TicketCoder.DTOs.Tickets
{
    public class UserTicketDTO
    {
        public string EventName { get; set; }
        public string EventType { get; set; }
        public DateTime PurchesDate { get; set; }
        public DateTime EventDateTime { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
