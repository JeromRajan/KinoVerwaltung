using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Repositories.Interfaces;

namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        //Ticket-Endpunkte
        #region Ticket
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] AddTicketDto ticket)
        {
            try
            {
                var hinzugefügtesTicket = await _ticketRepository.AddTicketAsync(ticket.BenutzerId, ticket.VorführungId, ticket.SitzId, ticket.ZahlungsmethodeId);
                return CreatedAtAction(nameof(AddTicket), new { id = hinzugefügtesTicket.TicketId }, hinzugefügtesTicket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketRepository.GetTicketsByBenutzerIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost("{TicketReferenzNummer}")]
        public async Task<IActionResult> ConfirmTicket(string TicketReferenzNummer)
        {
            try
            {
                var bestätigtesTicket = await _ticketRepository.ConfirmTicketAsync(TicketReferenzNummer);
                return Ok(bestätigtesTicket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }

}
