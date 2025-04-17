using Handmade.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Handmade.Context
{
    public class HandmadeContext(DbContextOptions<HandmadeContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship between User and Role
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            // Seed Roles
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "SuperUser", NormalizedName = "SUPERUSER", ConcurrencyStamp = "role-1-concurrency-stamp" },
                new IdentityRole<int> { Id = 2, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "role-2-concurrency-stamp" },
                new IdentityRole<int> { Id = 3, Name = "Seller", NormalizedName = "SELLER", ConcurrencyStamp = "role-3-concurrency-stamp" },
                new IdentityRole<int> { Id = 4, Name = "Buyer", NormalizedName = "BUYER", ConcurrencyStamp = "role-4-concurrency-stamp" }
            );

            // Seed User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Mahmoud",
                    LastName = "Elsonbaty",
                    UserName = "superuser",
                    NormalizedUserName = "SUPERUSER",
                    SecurityStamp = "UKLYGW2ZNKHUG4V7RHXHEJXCX3GU2W4M",
                    Email = "superuser@gmail.com",
                    NormalizedEmail = "SUPERUSER@GMAIL.COM",
                    PhoneNumber = "01118069749",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEPvFMPhaR/HYTNrj51YXvCxWmZsKXiZQ8XmZMwjAXMIzHZg81P2f+J2D+DCg6SObtQ==",
                    ConcurrencyStamp = "1bbac6ec-d816-4c7e-af9e-178f7676fbd3",
                    LockoutEnd = null,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                }
            );

            // Seed UserRoles
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 1, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 1, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 1, RoleId = 4 }
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
