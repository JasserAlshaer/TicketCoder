using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketCoder.Context;
using TicketCoder.DTOs.Tickets;
using TicketCoder.Interfaces;
using TicketCoder.Models.Enitites;

namespace TicketCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUserInterface
    {
        private readonly TicketCoderDbContext _ticketCoderDbContext;
        public UserController(TicketCoderDbContext context)
        {
            _ticketCoderDbContext = context;
        }
        #region Actions 


        [HttpGet]
        [Route("[action]")]
        public Task<IActionResult> GetTicketHistoryByUserIdAction(int Id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        [HttpPost]
        [Route("Buy")]
        public async Task<IActionResult> BuyTicketsAction(BuyTicketDTO dto)
        {
            try
            {
                await BuyTickets(dto);
                return new ObjectResult(null) { StatusCode = 201, Value = "BuyTicketsAction" };
            }
            catch (Exception ex)
            {
                return new ObjectResult(null) { StatusCode = 500, Value = $"Failed BuyTicketsAction  {ex.Message}" };
            }
        }
        #endregion

        [NonAction]
        public async Task BuyTickets(BuyTicketDTO dto)
        {
            for (int i = 0; i < dto.Qtn; i++)
            {
                UserTickets userTickets = new UserTickets();
                var entity = await _ticketCoderDbContext.Tickets.FindAsync(dto.TicketId);
                userTickets.Ticket = entity;
                userTickets.User = await _ticketCoderDbContext.Users.FindAsync(dto.UserId);
                int count = await _ticketCoderDbContext.UserTickets.Where(x => x.Ticket.Id == dto.TicketId).CountAsync();
                int serial = count == 0 ? 1 : count + 1;
                userTickets.TicketSerialNumber = userTickets.Ticket.TicketNumber + "-" + serial;
                userTickets.PurchesDate = DateTime.Now;
                userTickets.CreateionDate = DateTime.Now;
                userTickets.IsActive = true;
                await _ticketCoderDbContext.AddAsync(userTickets);
                await _ticketCoderDbContext.SaveChangesAsync();
                //entity.Qunatity
            }

        }
        [NonAction]
        public Task<List<UserTicketDTO>> GetTicketHistoryByUserId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
