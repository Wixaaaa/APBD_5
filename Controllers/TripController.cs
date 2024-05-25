using Microsoft.AspNetCore.Mvc;
using Zadanie7.DTO.Request;
using Zadanie7.Interfaces;

namespace Zadanie7.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripController(ITripService tripService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            try
            {
                return Ok(await tripService.GetTripsAsync());
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        [HttpPost("{idTrip:int}/clients")]
        public async Task<IActionResult> AddClientToTrip(int idTrip, ClientAssignmentDTO clientAssignmentDTO)
        {
            if (idTrip != clientAssignmentDTO.IdTrip) return BadRequest("IdTrip in URL and body must be the same.");
            try
            {
                return Ok(await tripService.AssignClientToTripAsync(clientAssignmentDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
