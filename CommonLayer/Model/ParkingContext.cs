using Microsoft.EntityFrameworkCore;

namespace CommonLayer.Model
{
    /// <summary>
    /// Middleware Component for the communication with
    /// database
    /// </summary>
    public class ParkingContext : DbContext
    {
        public ParkingContext(DbContextOptions<ParkingContext> options) : base(options)
        {

        }
        //Contains tabledata from the database
        public DbSet<ParkingPortal> parkingPortals { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<ParkingStatus> ParkingStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDetail>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
