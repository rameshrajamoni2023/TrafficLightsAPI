using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TrafficLightsAPI.Model;

namespace TrafficLightsAPI.Data
{
    //[TODO] to be integrated - just classes created - can be abstrated using a Interface 
    public class TrafficLightDbContext : DbContext
    {
        public TrafficLightDbContext(DbContextOptions<TrafficLightDbContext> options) : base(options)
        {
        }

        public DbSet<TrafficLight> TrafficLights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ORM Mappings
        }
    }

}
