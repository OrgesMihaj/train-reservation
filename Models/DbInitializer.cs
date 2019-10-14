using System;
using System.Linq;
using TrainReservation.Data;
using TrainReservation.Models;
using System.Threading.Tasks; 


namespace TrainReservation.Models
{
    public static class DbInitializer 
    {
        public static void Initialize(ApplicationDbContext context) 
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Train.Any())
            {
                // DB has been seeded
                return;   
            }

            var trains = new Train[]
            {
                new Train { TrainID = 1, Name="Carson",   Express = true,  BookSeats = true  },
                new Train { TrainID = 2, Name="Meredith", Express = false, BookSeats = true  },
                new Train { TrainID = 3, Name="Arturo",   Express = false, BookSeats = false },
                new Train { TrainID = 4, Name="Gytis",    Express = false, BookSeats = false } 
            };

            foreach (Train train in trains)
            {
                context.Train.Add(train);
            }
            
            context.SaveChanges();
        }
    }
}