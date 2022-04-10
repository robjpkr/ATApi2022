using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ATApi.Data.Models
{
    public class VehicleContext : DbContext
    {
        //public VehicleContext(DbContextOptions options) : base(options)
        //{ }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
