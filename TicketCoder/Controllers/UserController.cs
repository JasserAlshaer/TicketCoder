using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketCoder.DTOs.Tickets;
using TicketCoder.Interfaces;

namespace TicketCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUserInterface
    {
        [HttpGet]
        [Route("[action]")]
        public Task<List<UserTicketDTO>> GetTicketHistoryByUserId(int Id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("Buy")]
        public Task BuyTickets(BuyTicketDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}
