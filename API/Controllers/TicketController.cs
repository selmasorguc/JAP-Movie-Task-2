using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }


        [HttpPost("buy")]
        public async Task<ActionResult<ServiceResponse<Ticket>>> BuyTicket(TicketDto ticket)
        {
            var serviceResponse = await _ticketService.BuyTicket(ticket);
            if (serviceResponse.Data == null) return NotFound(serviceResponse);
            return Ok(await _ticketService.BuyTicket(ticket));
        }


    }
}