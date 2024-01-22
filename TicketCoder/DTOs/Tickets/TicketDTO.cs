using static TicketCoder.Helper.Enums.Enums;

namespace TicketCoder.DTOs.Tickets
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TicketNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Qunatity { get; set; }
        public int AvailableQunatity { get; set; }
        public float Price { get; set; }
        public string TicketType { get; set; }
        public int EventId { get; set; }
    }
}
