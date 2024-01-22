using TicketCoder.DTOs.Authantication;
using TicketCoder.DTOs.Events;
using TicketCoder.DTOs.Tickets;

namespace TicketCoder.Interfaces
{
    public interface IUserInterface
    {
        //BuyTicket
        Task BuyTickets(BuyTicketDTO dto);
        //GetTicketHistoryByUserId
        Task<List<UserTicketDTO>> GetTicketHistoryByUserId(int Id);
    }
}
