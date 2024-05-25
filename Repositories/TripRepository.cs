using Microsoft.EntityFrameworkCore;
using System;
using Zadanie7.Interfaces;
using Zadanie7.Models;

namespace Zadanie7.Repositories
{
    public class TripRepository(APBD5Context context) : ITripRepository
    {
        public async Task<IEnumerable<Trip>> GetTripsAsync()
        {
            var result = await context.Trips
                .Include(trip => trip.IdCountries)
                .Include(trip => trip.ClientTrips)
                .ThenInclude(clientTrip => clientTrip.IdClientNavigation)
                .OrderByDescending(trip => trip.DateFrom)
                .ToListAsync();
            if (result.Count == 0) throw new Exception("No trips found");
            return result;
        }

        public async Task<bool> TripExistsAsync(int id)
        {
            return await context.Trips.AnyAsync(trip => trip.IdTrip == id);
        }

        public async Task<int> AssignClientToTripAsync(ClientTrip clientTrip)
        {
            await context.ClientTrips.AddAsync(clientTrip);
            return await context.SaveChangesAsync();
        }

    }
}
