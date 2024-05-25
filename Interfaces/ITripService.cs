using Zadanie7.DTO.Request;
using Zadanie7.DTO.Response;

namespace Zadanie7.Interfaces
{
    public interface ITripService
    {
        Task<IEnumerable<TripDTO>> GetTripsAsync();
        Task<int> AssignClientToTripAsync(ClientAssignmentDTO clientAssignmentDTO);
    }
}
