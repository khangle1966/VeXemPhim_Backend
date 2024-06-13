namespace MovieTicketAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using MovieTicketAPI.Models;
    using MovieTicketAPI.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult<List<Ticket>> Get() => _ticketService.Get();

        [HttpGet("{id}", Name = "GetTicket")]
        public ActionResult<Ticket> Get(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPost]
        public ActionResult<Ticket> Create(Ticket ticket)
        {
            var createdTicket = _ticketService.Create(ticket);

            if (createdTicket == null)
            {
                return BadRequest("Seat is already booked or invalid details.");
            }

            return CreatedAtRoute("GetTicket", new { id = createdTicket.Id }, createdTicket);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _ticketService.Remove(ticket.Id);

            return NoContent();
        }
    }
}
