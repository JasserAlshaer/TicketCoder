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
        public Task<IActionResult> BuyTicketsAction(BuyTicketDTO dto)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
       
        [NonAction]
        public Task BuyTickets(BuyTicketDTO dto)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<List<UserTicketDTO>> GetTicketHistoryByUserId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
