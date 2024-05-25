using System.ComponentModel.DataAnnotations;

namespace Zadanie7.DTO.Request
{
    public class ClientAssignmentDTO
    {
        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(60)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(60)]
        public string Telephone { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{11}$")]
        public string Pesel { get; set; } = null!;

        [Required]
        public int IdTrip { get; set; }

        [Required]
        public string TripName { get; set; } = null!;

        public DateOnly? PaymentDate { get; set; }
    }
}
