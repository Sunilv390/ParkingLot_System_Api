using Microsoft.EntityFrameworkCore;

namespace CommonLayer.Model
{
    /// <summary>
    /// Middleware Component for the communication with
    /// database
    /// </summary>
    public class ParkingContext : DbContext
    {
        public ParkingContext(DbContextOptions options) : base(options)
        {

        }

        //Contains tabledata from the database
        public DbSet<ParkingPortal> parkingPortals { get; set; }
    }
}
