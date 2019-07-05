using System;
using System.Linq;
using ferrilata_devilline.Models;
using Microsoft.EntityFrameworkCore;

namespace ferrilata_devilline.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AuxPitch> PitchTable { get; set; }


        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

