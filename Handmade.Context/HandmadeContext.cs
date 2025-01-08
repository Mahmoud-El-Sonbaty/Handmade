using Handmade.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Handmade.Context
{
    //public class HandmadeContext : IdentityDbContext<CustomUser, IdentityRole<int>, int>
    //{
    //    public HandmadeContext(DbContextOptions<HandmadeContext> options) : base(options) { }
    //}
    public class HandmadeContext(DbContextOptions<HandmadeContext> options) : IdentityDbContext<Users, IdentityRole<int>, int>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed Roles
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "SuperUser", NormalizedName = "SUPERUSER" },
                new IdentityRole<int> { Id = 2, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 3, Name = "Seller", NormalizedName = "SELLER" },
                new IdentityRole<int> { Id = 4, Name = "Buyer", NormalizedName = "BUYER" }
            );
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.Now;
                    entity.Entity.CreatedBy = 1;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = DateTime.Now;
                    entity.Entity.UpdatedBy = 1;
                }
            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.Now;
                    entity.Entity.CreatedBy = 1;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = DateTime.Now;
                    entity.Entity.UpdatedBy = 1;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
