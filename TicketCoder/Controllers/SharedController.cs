using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TicketCoder.Context;
using TicketCoder.DTOs.Authantication;
using TicketCoder.DTOs.Events;
using TicketCoder.DTOs.Tickets;
using TicketCoder.Interfaces;
using TicketCoder.Models.Enitites;
using static TicketCoder.Helper.Enums.Enums;

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
        [Route("[action]/{Id}")]
        public async Task<IActionResult> GetEventDetailsAction(int Id)
        {
            try
            {
                return Ok(await GetEventDetails(Id));
            }
            catch (Exception ex)
            {
                return new ObjectResult(null) { StatusCode = 500, Value = "Something Went Wrong" };
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEventsAction(string? title, DateTime? time, int? type)
        {
            try
            {
                return Ok(await GetEvents(title, time, type));
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
        public async Task<IActionResult> CreateNewAccountAction(RegistrationDTO dto)
        {

            try
            {
                await CreateNewAccount(dto);
                return new ObjectResult(null) { StatusCode = 201, Value = "New Account Has Been Activated" };
            }
            catch (Exception ex)
            {
                return new ObjectResult(null) { StatusCode = 500, Value = $"Failed Createing Account  {ex.Message}" };
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginAction(LoginDTO dto)
        {
            try
            {
                await Login(dto);
                return new ObjectResult(null) { StatusCode = 201, Value = "Login Success" };
            }
            catch (Exception ex)
            {
                return new ObjectResult(null) { StatusCode = 500, Value = $"Login Failed {ex.Message}" };
            }
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> ResetPasswordAction(ResetPasswordDTO dto)
        {
            try
            {
                await ResetPassword(dto);
                return new ObjectResult(null) { StatusCode = 201, Value = "ResetPassword Success" };
            }
            catch (Exception ex)
            {
                return new ObjectResult(null) { StatusCode = 500, Value = $"ResetPassword Failed {ex.Message}" };
            }
        }
        #endregion

        #region Implementations
        [NonAction]
        public async Task CreateNewAccount(RegistrationDTO dto)
        {
            //validation
            if (string.IsNullOrEmpty(dto.Email))
                throw new Exception("Email Is Required");
            if (string.IsNullOrEmpty(dto.Phone))
                throw new Exception("Phone Is Required");
            if (string.IsNullOrEmpty(dto.Password))
                throw new Exception("Password Is Required");
            if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("Name Is Required");
            User user = new User();
            user.Email = dto.Email;
            user.Phone = dto.Phone;
            user.Password = dto.Password;
            user.Name = dto.Name;
            user.IsActive = true;
            user.CreateionDate = DateTime.Now;
            await _ticketCoderDbContext.AddAsync(user);
            await _ticketCoderDbContext.SaveChangesAsync();
        }
        [NonAction]
        public async Task<EventDetailsDTO> GetEventDetails(int Id)
        {
            var query = await (from e in _ticketCoderDbContext.Events
                               where e.Id == Id
                               select new EventDetailsDTO
                               {
                                   EventId = e.Id,
                                   Title = e.Title,
                                   Description = e.Description,
                                   Type = e.EventType.ToString(),
                                   EventDate = e.EventTime,
                                   Address = e.Address
                               }).SingleAsync();
            query.AvailableTicketCount = 0;
            query.TakingSeats = 0;
            var lowestTicket = (await _ticketCoderDbContext.Tickets
                    .Where(t => t.Event.Id == query.EventId)
                    .OrderBy(x => x.Price)
                    .FirstOrDefaultAsync());
            query.StartingPrice = lowestTicket == null ? 0 : lowestTicket.Price;
            query.AvailableTicketCount = await _ticketCoderDbContext.Tickets
                .Where(t => t.Event.Id == query.EventId).SumAsync(t => t.Qunatity);
            query.TakingSeats = await _ticketCoderDbContext.UserTickets.Where(x => x.Ticket.Event.Id == Id)
                .CountAsync();
            //ticketQuery
            var ticketQuery = await (from tick in _ticketCoderDbContext.Tickets
                          where tick.Event.Id == Id
                          select new TicketDTO
                          {
                              Id = tick.Id,
                              Title = tick.Title,
                              Description = tick.Description,
                              TicketNumber = tick.TicketNumber,
                              ExpirationDate = tick.ExpirationDate,
                              Qunatity = tick.Qunatity,
                              Price = tick.Price,
                              TicketType = tick.TicketType.ToString(),
                              EventId = Id
                          }).ToListAsync();
            ticketQuery.ForEach(async ticket 
                => ticket.AvailableQunatity = ticket.Qunatity -
                (await _ticketCoderDbContext.UserTickets.Where(x => x.Ticket.Id == ticket.Id)
                .CountAsync()));
            query.Tickets = ticketQuery;
            return query;
        }
        [NonAction]
        public async Task<List<EventInfoDTO>> GetEvents(string? title, DateTime? time, int? type)
        {
            EventType foo = EventType.none;
            if (Enum.IsDefined(typeof(EventType), type == null ? 1000 : type))
                foo = (EventType)Enum.ToObject(typeof(EventType), type);
            bool flag = title == null && time == null && type == null ? true : false;
            //var query =await  _ticketCoderDbContext.Events
            //    .Where(x=>x.Title.Contains(title) ||
            //    x.EventTime >= time || (int)x.EventType == (int)type
            //    || flag
            //    ).ToListAsync(); //get all events as IEmunrable
            var query = from t in _ticketCoderDbContext.Events
                        where t.EventType == foo
                        //|| t.Title.Contains(title)
                        //|| t.EventTime >= time
                        || flag
                        select t;
            List<EventInfoDTO> result = new List<EventInfoDTO>();
            foreach (var evt in query)
            {
                EventInfoDTO temp = new EventInfoDTO();
                temp.Title = evt.Title;
                temp.EventId = evt.Id;
                temp.Description = evt.Description;
                temp.Type = evt.EventType.ToString();
                //get lowest price for ticket for this event 
                var lowestTicket = (await _ticketCoderDbContext.Tickets
                    .Where(t => t.Event.Id == evt.Id)
                    .OrderBy(x => x.Price)
                    .FirstOrDefaultAsync());
                temp.StartingPrice = lowestTicket == null ? 0 : lowestTicket.Price;
                temp.EventDate = evt.EventTime.Date;
                temp.AvailableTicketCount = await _ticketCoderDbContext.Tickets
                    .Where(t => t.Event.Id == evt.Id).SumAsync(t => t.Qunatity);
                result.Add(temp);

            }
            return result;
        }
        [NonAction]
        public async Task Login(LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                throw new Exception("Email and Password are required");
            var login = await _ticketCoderDbContext.Users
                 .Where(x => x.Email.Equals(dto.Email) && x.Password.Equals(dto.Password))
                 .SingleOrDefaultAsync();
            if (login == null)
            {
                throw new Exception("Email Or Password Is Not Correct");
            }
        }
        [NonAction]
        public async Task ResetPassword(ResetPasswordDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Phone))
                throw new Exception("Email and Phone are required");
            var user = await _ticketCoderDbContext.Users.Where(x => x.Email.Equals(dto.Email)
            && x.Phone.Equals(dto.Phone)).SingleOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            else
            {
                if (string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.ConfirmPassword))
                    throw new Exception("Password and ConfirmPassword are required");
                else
                {
                    if (dto.Password.Equals(dto.ConfirmPassword))
                    {
                        user.Password = dto.ConfirmPassword;
                        _ticketCoderDbContext.Update(user);
                        await _ticketCoderDbContext.SaveChangesAsync();
                    }
                }

            }
        }
        #endregion
    }
}
