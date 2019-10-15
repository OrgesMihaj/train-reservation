using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainReservation.Models;

namespace TrainReservation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TrainReservation.Models.Train> Trains { get; set; }
        public DbSet<TrainReservation.Models.Journey> Journeys { get; set; }
    }
}
