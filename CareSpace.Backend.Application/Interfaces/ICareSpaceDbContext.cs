using CareSpace.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CareSpace.Backend.Application.Interfaces
{
    public interface ICareSpaceDbContext
    {
        DbSet<User> Users { get; }
        DbSet<ServiceCategory> ServiceCategories { get; }
        DbSet<Service> Services { get; }
        DbSet<AvailableService> AvailableServices { get; }
        DbSet<TimeSlot> TimeSlots { get; }
        DbSet<ServiceCentre> ServiceCentres { get; }
        DbSet<ServiceSchedule> ServiceSchedules { get; }
        DbSet<Appointment> Appointments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}