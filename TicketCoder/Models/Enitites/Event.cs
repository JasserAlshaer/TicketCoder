using static TicketCoder.Helper.Enums.Enums;

namespace TicketCoder.Models.Enitites
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public EventType EventType { get; set; }
        public DateTime EventTime { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
    }
}
