using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CareSpace.Backend.Infrastructure.Persistence
{
    public class CareSpaceDbContext : DbContext, ICareSpaceDbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<AvailableService> AvailableServices { get; set; } = null!;

        public DbSet<TimeSlot> TimeSlots { get; set; } = null!;

        public DbSet<ServiceCentre> ServiceCentres { get; set; } = null!;

        public DbSet<ServiceSchedule> ServiceSchedules { get; set; } = null!;

        public DbSet<Appointment> Appointments { get; set; } = null!;

        public CareSpaceDbContext(DbContextOptions<CareSpaceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CareSpaceDbContext).Assembly);

            modelBuilder.Entity<User>()
                .HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<ServiceCategory>()
                .HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<Service>()
                .HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<AvailableService>()
                .HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<TimeSlot>()
                .HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<ServiceCentre>()
                .HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<ServiceSchedule>()
                .HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<Appointment>()
                .HasQueryFilter(e => e.DeletedAt == null);

            base.OnModelCreating(modelBuilder);
        }
    }
}