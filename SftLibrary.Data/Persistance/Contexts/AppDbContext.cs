using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SftLib.Data.Domain.Models;
using SftLibrary.Data.Domain.Models;
using System;

namespace SftLib.Data.Persistance.Contexts
{
    public class AppDbContext : IdentityDbContext<User, Role,
                               int, IdentityUserClaim<int>,
                               UserRole, IdentityUserLogin<int>,
                               IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId).IsRequired();

                userRole.HasOne(ur => ur.User).WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId).IsRequired();
            });

            modelBuilder.Entity<Status>().HasKey(x => x.Id);
            modelBuilder.Entity<Status>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Status>().Property(x => x.Name).IsRequired().HasMaxLength(20);



            modelBuilder.Entity<Status>().HasData(
                new Status { Id = -1, Name = "Checked Out" },
                new Status { Id = -2, Name = "Available" },
                new Status { Id = -3, Name = "On Hold" }
                );


            modelBuilder.Entity<Book>().HasKey(x => x.Id);
            modelBuilder.Entity<Book>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().Property(x => x.Title).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Book>().Property(x => x.Author).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Book>().Property(x => x.Year).IsRequired();
            modelBuilder.Entity<Book>().Property(x => x.StatusId).IsRequired();


            modelBuilder.Entity<Book>().HasData(
                new Book { Id = -1, Author = "Greg", StatusId = -1, Title = "The Redbreast", Year = DateTime.Now.Year },
                new Book { Id = -2, Author = "Simon", StatusId = -2, Title = "Two States", Year = DateTime.Now.Year }
                );
        }


    }
}
