namespace Zadanie7.DTO.Response
{
    public class TripDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateOnly DateFrom { get; set; }

        public DateOnly DateTo { get; set; }

        public int MaxPeople { get; set; }

        public IEnumerable<CountryDTO> Countries { get; set; } = null!;

        public IEnumerable<ClientDTO> Clients { get; set; } = null!;
    }
}
