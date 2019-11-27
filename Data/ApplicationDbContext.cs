using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainReservation.Models;

namespace TrainReservation.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TrainReservation.Models.Train> Trains { get; set; }
        public DbSet<TrainReservation.Models.Journey> Journeys { get; set; }
        public DbSet<TrainReservation.Models.Booking> Bookings { get; set; }
        public DbSet<TrainReservation.Models.Seat> Seats { get; set; }
        public DbSet<TrainReservation.Models.Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });


            // [Booking:Seats] One-to-Many relationship
            modelBuilder.Entity<Booking>().HasMany(c => c.Seats).WithOne(s => s.Booking);
            
            modelBuilder.Entity<Journey>().HasMany(c => c.Seats).WithOne(s => s.Journey);



            // [Journey:Booking] One-to-Many relationship
            modelBuilder.Entity<Journey>().HasMany(j => j.Bookings).WithOne(b => b.Journey);
        }

        public DbSet<TrainReservation.Models.Coupon> Coupon { get; set; }
    }
}
