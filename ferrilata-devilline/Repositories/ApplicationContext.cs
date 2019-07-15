using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DAOs.JsonHelper;
using Microsoft.EntityFrameworkCore;


namespace ferrilata_devilline.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<JsonSchemaForDevilline> JsonSchemas { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Pitch> Pitches { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<UserLevel>()
                .HasKey(e => new { e.UserId, e.LevelId });
        }
    }
}