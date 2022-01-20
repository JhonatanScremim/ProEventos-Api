using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Repository.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Event> Event { get; set; }
        public DbSet<Lecturer> Lecturer { get; set; }
        public DbSet<EventLecturer> EventLecturer { get; set; }
        public DbSet<SocialNetwork> SocialNetwork { get; set; }
        public DbSet<Batch> Batch { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<EventLecturer>()
                .HasKey(x => new {x.EventId, x.LecturerId});

            modelBuilder.Entity<Event>()
                .HasMany(e => e.SocialNetworks)
                .WithOne(rs => rs.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lecturer>()
                .HasMany(e => e.SocialNetworks)
                .WithOne(rs => rs.Lecturer)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}