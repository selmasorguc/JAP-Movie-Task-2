using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        //Buying new ticket functionality
        [HttpPost("buy")]
        public async Task<ActionResult<ServiceResponse<AddTicketDto>>> BuyTicket(TicketDto ticket)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var serviceResponse = await _ticketService.BuyTicket(ticket, username);

            if (serviceResponse.Data == null) return NotFound(serviceResponse);

            return Ok(serviceResponse);
        }

        //Not required functionality in JAP Task
        //Added just to test adding screeing to DB 
        [HttpPost("addScreening")]
        public async Task<ActionResult<ServiceResponse<AddScreeningDto>>> AddScreening(AddScreeningDto screening)
        {
            var serviceResponse = await _ticketService.CreateScreening(screening);

            if (serviceResponse.Data == null) return NotFound(serviceResponse);

            return Ok(serviceResponse);
        }
    }
}