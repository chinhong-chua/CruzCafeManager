using CafeBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CafeBackend.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
            
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cafe>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(c => c.Location)
                    .IsRequired();

            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                     .HasMaxLength(10)
                     .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.EmailAddress)
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired();

                entity.Property(e => e.Gender)
                    .IsRequired();

                entity.HasOne(e => e.Cafe)
                     .WithMany(c => c.Employees)
                     .HasForeignKey(e => e.CafeId)
                     .OnDelete(DeleteBehavior.SetNull);

                // Unique constraint to ensure employee works at only one cafe
                entity.HasIndex(e => e.Id).IsUnique();
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach(var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q=> q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.ModifiedDate = DateTime.Now;
                if(entry.State == EntityState.Added)
                    entry.Entity.CreatedDate = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
