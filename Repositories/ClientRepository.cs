using Microsoft.EntityFrameworkCore;
using System;
using Zadanie7.Interfaces;
using Zadanie7.Models;

namespace Zadanie7.Repositories
{
    public class ClientRepository(APBD5Context context) : IClientRepository
    {
        public async Task<int> DeleteClientAsync(int id)
        {
            return await context.Clients
                .Where(client => client.IdClient == id)
                .ExecuteDeleteAsync();
        }

        public async Task<int> GetClientTripsCountAsync(int id)
        {
            return await context.ClientTrips
                .Where(clientTrip => clientTrip.IdClient == id)
                .CountAsync();
        }

        public async Task<bool> ClientWithPeselExistsAsync(string pesel)
        {
            return await context.Clients.AnyAsync(client => client.Pesel == pesel);
        }

        public async Task<int> GetLastClientIdAsync()
        {
            return await context.Clients
                .OrderByDescending(client => client.IdClient)
                .Select(client => client.IdClient)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddClientAsync(Client client)
        {
            await context.Clients.AddAsync(client);
            return await context.SaveChangesAsync();
        }

        public async Task<bool> IsClientAssociatedWithTripAsync(int idClient, int idTrip)
        {
            return await context.ClientTrips
                .AnyAsync(clientTrip => clientTrip.IdClient == idClient && clientTrip.IdTrip == idTrip);
        }

        public async Task<int> GetClientIdByPeselAsync(string pesel)
        {
            return await context.Clients
                .Where(client => client.Pesel == pesel)
                .Select(client => client.IdClient)
                .FirstOrDefaultAsync();
        }
    }
}
