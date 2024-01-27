using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketCoder.Context;
using TicketCoder.DTOs.Authantication;
using TicketCoder.DTOs.Events;
using TicketCoder.Interfaces;

namespace TicketCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedController : ControllerBase, ISharedInterface
    {
        private readonly TicketCoderDbContext _ticketCoderDbContext;
        public SharedController(TicketCoderDbContext context)
        {
            _ticketCoderDbContext = context;
        }
        #region Get Information
        [HttpGet]
        [Route("[action]")]
        public Task<IActionResult> GetEventDetailsAction(int Id)
        {
            throw new NotImplementedException();
            try
            {
                //return Ok();
            }
            catch (Exception ex)
            {
                //return Unauthorized();
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEventsAction(string? title, DateTime? time, int? type)
        {
            try
            {
                return Ok(await GetEvents(title,time,type));
            }
            catch (Exception ex)
            {
                throw new Exception("Test Exception");
            }
        }
        #endregion

        #region Authantication

        [HttpPost]
        [Route("[action]")]
        public Task<IActionResult> CreateNewAccountAction(RegistrationDTO dto)
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
        [Route("[action]")]
        public Task<IActionResult> LoginAction(LoginDTO dto)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        [HttpPut]
        [Route("[action]")]
        public Task<IActionResult> ResetPasswordAction(ResetPasswordDTO dto)
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

        #region Implementations
        [NonAction]
        public Task CreateNewAccount(RegistrationDTO dto)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<EventDetailsDTO> GetEventDetails(int Id)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public async Task<List<EventInfoDTO>> GetEvents(string? title, DateTime? time, int? type)
        {
            bool flag = title==null && time == null && type==null?true:false;
            var query =await  _ticketCoderDbContext.Events
                .Where(x=>x.Title.Contains(title) ||
                x.EventTime >= time || (int)x.EventType == (int)type
                || flag
                ).ToListAsync(); //get all events as IEmunrable
            List < EventInfoDTO > result = new List< EventInfoDTO >();
            foreach(var evt in query)
            {
                EventInfoDTO temp= new EventInfoDTO();
                temp.Title = evt.Title;
                temp.EventId = evt.Id;
                temp.Description = evt.Description;
                temp.Type = evt.EventType.ToString();
                //get lowest price for ticket for this event 
                var lowestTicket = (await _ticketCoderDbContext.Tickets
                    .Where(t => t.Event.Id == evt.Id)
                    .OrderBy(x=>x.Price)
                    .FirstOrDefaultAsync());
                temp.StartingPrice = lowestTicket==null?0: lowestTicket.Price;
                temp.EventDate = evt.EventTime.Date;
                temp.AvailableTicketCount = await _ticketCoderDbContext.Tickets
                    .Where(t => t.Event.Id == evt.Id).SumAsync(t=>t.Qunatity);
                result.Add(temp);

            }
            return result;
        }
        [NonAction]
        public Task Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task ResetPassword(ResetPasswordDTO dto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
