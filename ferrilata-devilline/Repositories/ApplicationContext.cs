using ferrilata_devilline.Models.DAOs;
using Microsoft.EntityFrameworkCore;


namespace ferrilata_devilline.Repositories
{
    public class ApplicationContext : DbContext
    {
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


            modelBuilder.Entity<Badge>().HasMany(b => b.Levels).WithOne(l => l.Badge)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Level>().HasMany(l => l.UserLevels).WithOne(l => l.Level)
                .HasForeignKey(l => l.LevelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Level>().HasMany(l => l.Pitches).WithOne(p => p.Level)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pitch>().HasMany(p => p.Reviews).WithOne(r => r.Pitch)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLevel>()
                .HasKey(e => new {e.UserId, e.LevelId});
        }
    }
}