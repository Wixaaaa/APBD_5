using Microsoft.AspNetCore.Mvc;
using Zadanie7.Interfaces;

namespace Zadanie7.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                return Ok("Removed " + await clientService.DeleteClientAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
