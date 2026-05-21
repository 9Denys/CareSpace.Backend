using CareSpace.Backend.Domain.Common;
using CareSpace.Backend.Domain.Enums;

namespace CareSpace.Backend.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public Role Role { get; set; } = Role.User;

        public required string PasswordHash { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public virtual ICollection<AvailableService> AvailableServices { get; set; } = new List<AvailableService>();

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}