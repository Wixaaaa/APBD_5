using Zadanie7.Interfaces;

namespace Zadanie7.Services
{
    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        public async Task<int> DeleteClientAsync(int id)
        {
            var hasTrips = await clientRepository.GetClientTripsCountAsync(id) > 0;
            if (hasTrips) throw new Exception("Cannot delete client with trips.");
            return await clientRepository.DeleteClientAsync(id);
        }
    }
}
