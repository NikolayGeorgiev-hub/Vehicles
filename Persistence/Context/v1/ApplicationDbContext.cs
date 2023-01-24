using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities.v1;


namespace Persistence.Context.v1
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            VehicleConfiguration(modelBuilder.Entity<Vehicle>());
            TownConfiguration(modelBuilder.Entity<Town>());
            VehicleTypeConfiguration(modelBuilder.Entity<VehicleType>());

            base.OnModelCreating(modelBuilder);
        }


        private void VehicleConfiguration(EntityTypeBuilder<Vehicle> vehicle)
        {
            vehicle
                .HasKey(x => x.Id);

            vehicle
                .Property(x => x.EngineCapacity)
                .IsRequired();

            vehicle
                .Property(x => x.VehicleAge)
                .IsRequired();

            vehicle
                .Property(x => x.Purpose)
                .IsRequired();

            vehicle
                .HasOne(x => x.Town)
                .WithMany(x => x.Vehicles)
                .HasForeignKey(x => x.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            vehicle
                .HasOne(x => x.VehicleType)
                .WithMany()
                .HasForeignKey(x => x.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            vehicle
                .HasOne(x => x.Owner)
                .WithMany(x => x.Vehicles)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void TownConfiguration(EntityTypeBuilder<Town> town)
        {
            town
                .HasKey(x => x.Id);

            town
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            town
                .Property(x => x.Postcode)
                .IsRequired()
                .HasMaxLength(10);
        }

        private void VehicleTypeConfiguration(EntityTypeBuilder<VehicleType> vehicleType)
        {
            vehicleType
                .HasKey(x => x.Id);

            vehicleType
                .Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode();
        }

    }
}
