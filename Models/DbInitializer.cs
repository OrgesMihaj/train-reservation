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
            if (context.Trains.Any())
            {
                // DB has been seeded
                return;   
            }

            var trains = new Train[]
            {
                new Train { TrainID = 1, Name="Carson",   Express = true  },
                new Train { TrainID = 2, Name="Meredith", Express = false },
                new Train { TrainID = 3, Name="Arturo",   Express = false },
                new Train { TrainID = 4, Name="Gytis",    Express = false } 
            };

            foreach (Train train in trains)
            {
                context.Trains.Add(train);
            }

            var journeys = new Journey[]
            {
                new Journey { 
                    JourneyID = 1, 
                    Departure="Sofia", 
                    Destination = "Blagoevgrad", 
                    DepartureTime = new DateTime(2019, 11, 21, 13, 0, 0),
                    ArrivalTime = new DateTime(2019, 11, 21, 15, 0, 0),
                    Price = 5,
                    AllowSeatReservation = false,
                    TrainID = 1
                },
                new Journey { 
                    JourneyID = 2, 
                    Departure="Blagoevgrad", 
                    Destination = "Sofia", 
                    DepartureTime = new DateTime(2019, 11, 21, 15, 30, 0),
                    ArrivalTime = new DateTime(2019, 11, 21, 17, 30, 0),
                    Price = 5,
                    AllowSeatReservation = false,
                    TrainID = 1
                },
                new Journey { 
                    JourneyID = 3, 
                    Departure="Sofia", 
                    Destination = "Plovdiv", 
                    DepartureTime = new DateTime(2019, 11, 21, 10, 0, 0),
                    ArrivalTime = new DateTime(2019, 11, 21, 12, 20, 0),
                    Price = 7,
                    AllowSeatReservation = false,
                    TrainID = 2
                },
                new Journey { 
                    JourneyID = 4, 
                    Departure="Sofia", 
                    Destination = "Burgas", 
                    DepartureTime = new DateTime(2019, 11, 21, 8, 0, 0),
                    ArrivalTime = new DateTime(2019, 11, 21, 13, 35, 0),
                    Price = 15,
                    AllowSeatReservation = true,
                    TrainID = 3
                }
            };

            foreach (Journey journey in journeys)
            {
                context.Journeys.Add(journey);
            }
            
            context.SaveChanges();
        }
    }
}