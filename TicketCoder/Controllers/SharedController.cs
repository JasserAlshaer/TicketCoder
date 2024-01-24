using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketCoder.DTOs.Authantication;
using TicketCoder.DTOs.Events;
using TicketCoder.Interfaces;

namespace TicketCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedController : ControllerBase, ISharedInterface
    {
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
        public Task<IActionResult> GetEventsAction(string? title, DateTime? time, int? type)
        {
            try
            {
                throw new NotImplementedException();
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
        public Task<List<EventInfoDTO>> GetEvents(string? title, DateTime? time, int? type)
        {
            throw new NotImplementedException();
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
