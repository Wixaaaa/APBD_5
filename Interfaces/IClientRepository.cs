using Zadanie7.Models;

namespace Zadanie7.Interfaces
{
    public interface IClientRepository
    {
        Task<int> DeleteClientAsync(int id);
        Task<int> GetClientTripsCountAsync(int id);
        Task<bool> ClientWithPeselExistsAsync(string pesel);
        Task<int> GetLastClientIdAsync();
        Task<int> AddClientAsync(Client client);
        Task<bool> IsClientAssociatedWithTripAsync(int idClient, int idTrip);
        Task<int> GetClientIdByPeselAsync(string pesel);
    }
}
