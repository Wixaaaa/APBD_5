using Zadanie7.DTO.Request;
using Zadanie7.DTO.Response;
using Zadanie7.Interfaces;
using Zadanie7.Models;
using Zadanie7.Repositories;

namespace Zadanie7.Services
{
    public class TripService(TripRepository tripRepository, IClientRepository clientRepository) : ITripService
    {
        public async Task<IEnumerable<TripDTO>> GetTripsAsync()
        {
            return (await tripRepository.GetTripsAsync())
                .Select(trip => new TripDTO
                {
                    Name = trip.Name,
                    Description = trip.Description,
                    DateFrom = DateOnly.FromDateTime(trip.DateFrom),
                    DateTo = DateOnly.FromDateTime(trip.DateTo),
                    MaxPeople = trip.MaxPeople,
                    Countries = trip.IdCountries.Select(country => new CountryDTO
                    {
                        Name = country.Name
                    })
                        .ToList(),
                    Clients = trip.ClientTrips.Select(clientTrip => new ClientDTO
                    {
                        FirstName = clientTrip.IdClientNavigation.FirstName,
                        LastName = clientTrip.IdClientNavigation.LastName
                    })
                        .ToList()
                });
        }

        public async Task<int> AssignClientToTripAsync(ClientAssignmentDTO clientAssignmentDTO)
        {
            var clientExists = await clientRepository.ClientWithPeselExistsAsync(clientAssignmentDTO.Pesel);
            int idClient;

            if (!clientExists)
            {
                idClient = await clientRepository.GetLastClientIdAsync() + 1;
                await clientRepository.AddClientAsync(new Client
                {
                    IdClient = idClient,
                    FirstName = clientAssignmentDTO.FirstName,
                    LastName = clientAssignmentDTO.LastName,
                    Email = clientAssignmentDTO.Email,
                    Telephone = clientAssignmentDTO.Telephone,
                    Pesel = clientAssignmentDTO.Pesel
                });
            }
            else
            {
                idClient = await clientRepository.GetClientIdByPeselAsync(clientAssignmentDTO.Pesel);
            }

            var isClientAssociatedWithTrip =
                await clientRepository.IsClientAssociatedWithTripAsync(idClient, clientAssignmentDTO.IdTrip);
            if (isClientAssociatedWithTrip) throw new Exception("Client is already associated with this trip.");

            var tripExists = await tripRepository.TripExistsAsync(clientAssignmentDTO.IdTrip);
            if (!tripExists) throw new Exception("Trip does not exist.");

            return await tripRepository.AssignClientToTripAsync(new ClientTrip
            {
                IdClient = idClient,
                IdTrip = clientAssignmentDTO.IdTrip,
                PaymentDate = clientAssignmentDTO.PaymentDate?.ToDateTime(TimeOnly.MinValue),
                RegisteredAt = DateTime.Now
            });
        }
    }
}
