using TicketCoder.DTOs.Tickets;

namespace TicketCoder.DTOs.Events
{
    public class EventDetailsDTO
    {
        //get basic info
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float StartingPrice { get; set; }
        public DateTime EventDate { get; set; }
        public int AvailableTicketCount { get; set; }
        //get detailed info about event
        public string Address { get; set; }
        public int TakingSeats { get; set; }
        //get tickets
        public List<TicketDTO> Tickets { get; set; }
    }
}
