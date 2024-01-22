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
        public Task<EventDetailsDTO> GetEventDetails(int Id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("[action]")]
        public Task<List<EventInfoDTO>> GetEvents(string? title, DateTime? time, int? type)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Authantication

        [HttpPost]
        [Route("[action]")]
        public Task CreateNewAccount(RegistrationDTO dto)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("[action]")]
        public Task Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        [Route("[action]")]
        public Task ResetPassword(ResetPasswordDTO dto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
