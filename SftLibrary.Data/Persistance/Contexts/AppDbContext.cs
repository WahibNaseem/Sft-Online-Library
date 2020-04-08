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
        public DbSet<Checkout> CheckOuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*Configure Identity UserRole With Role entity
             * and User entity
             */
            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId).IsRequired();

                userRole.HasOne(ur => ur.User).WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId).IsRequired();
            });


            /*  Configure Book CheckOut entity with 
             *  User entity and book entity
             */

            modelBuilder.Entity<Checkout>(checkout =>
            {
                checkout.HasKey(x => x.Id);

                checkout.HasOne(x => x.User).WithMany(u => u.CheckOuts)
                        .HasForeignKey(x => x.CheckoutUserId);                

            });



            /* Configure Status entity
             */
            modelBuilder.Entity<Status>().HasKey(x => x.Id);
            modelBuilder.Entity<Status>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Status>().Property(x => x.Name).IsRequired().HasMaxLength(20);



            modelBuilder.Entity<Status>().HasData(
                new Status { Id = -1, Name = "Checked Out" },
                new Status { Id = -2, Name = "Available" },
                new Status { Id = -3, Name = "On Hold" }
                );



            /*
             * Configure book entity
             */

            modelBuilder.Entity<Book>(book =>
            {
                book.HasKey(x => x.Id);
                book.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
                book.Property(x => x.Title).IsRequired().HasMaxLength(50);
                book.Property(x => x.Author).IsRequired().HasMaxLength(30);
                book.Property(x => x.Year).IsRequired();
                book.Property(x => x.StatusId).IsRequired();

            });


            modelBuilder.Entity<Book>().HasData(
                new Book { Id = -1, Author = "Greg", StatusId = -1, Title = "The Redbreast", Year = DateTime.Now.Year },
                new Book { Id = -2, Author = "Simon", StatusId = -2, Title = "Two States", Year = DateTime.Now.Year }
                );
        }


    }
}
