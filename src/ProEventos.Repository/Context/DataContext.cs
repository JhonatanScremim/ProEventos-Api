using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Repository.Context
{
    public class DataContext : IdentityDbContext<User, Role, int,
                                                IdentityUserClaim<int>, UserRole,
                                                IdentityUserLogin<int>, IdentityRoleClaim<int>, 
                                                IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Event> Event { get; set; }
        public DbSet<Lecturer> Lecturer { get; set; }
        public DbSet<EventLecturer> EventLecturer { get; set; }
        public DbSet<SocialNetwork> SocialNetwork { get; set; }
        public DbSet<Batch> Batch { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => {
                userRole.HasKey(x => new {x.UserId, x.RoleId});

                userRole.HasOne(x => x.Role)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleId)
                    .IsRequired();
                
                userRole.HasOne(x => x.User)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired();
            });

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