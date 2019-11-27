using System;
using System.Linq;
using TrainReservation.Data;
using TrainReservation.Models;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Identity;

namespace TrainReservation.Models
{
    public static class DbInitializer 
    {
        public static void Initialize(ApplicationDbContext context, UserManager<AppUser> userManager) 
        {
            string password = "P@ssword1";

            // list of admin users
            string[,] users = new string[,]
            {
                {"Orges", "Mihaj", "onm170@aubg.edu"},
                {"Lazarot", "Shyta", "lns170@aubg.edu"},
                {"Besar", "Limani", "bnl160@aubg.edu"}
            };

            for (int i = 0; i < 3; i++) {

                // check if user already exists
                if (userManager.FindByEmailAsync(users[i, 2]).Result == null)
                {
                    AppUser AppUser = new AppUser
                    {
                        UserName = users[i, 2],
                        Email = users[i, 2],
                        FirstName = users[i, 0],
                        LastName = users[i, 1],
                    };

                    // add new user
                    IdentityResult result = userManager.CreateAsync(AppUser, password).Result;

                    if (result.Succeeded)
                    {
                        // assign admin role to user
                        userManager.AddToRoleAsync(AppUser, "Admin").Wait();
                    }
                }       
            }

            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Trains.Any() && context.Journeys.Any() && context.Coupons.Any())
            {
                // DB has been seeded
                return;   
            }

            var trains = new Train[]
            {
                new Train { TrainID = 1, Name="Carson",   Capacity = 32,  Express = true  },
                new Train { TrainID = 2, Name="Meredith", Capacity = 64,  Express = false },
                new Train { TrainID = 3, Name="Arturo",   Capacity = 128, Express = false },
                new Train { TrainID = 4, Name="Gytis",    Capacity = 64,  Express = false } 
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
                    DepartureTime = DateTime.Now.AddDays(1),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(1),
                    Price = 5,
                    AllowSeatReservation = false,
                    TrainID = 1
                },
                new Journey { 
                    JourneyID = 2, 
                    Departure="Blagoevgrad", 
                    Destination = "Sofia", 
                    DepartureTime = DateTime.Now.AddDays(1),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(1),
                    Price = 5,
                    AllowSeatReservation = false,
                    TrainID = 1
                },
                new Journey { 
                    JourneyID = 3, 
                    Departure="Sofia", 
                    Destination = "Plovdiv", 
                    DepartureTime = DateTime.Now.AddDays(1),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(3),
                    Price = 7,
                    AllowSeatReservation = false,
                    TrainID = 2
                },
                new Journey { 
                    JourneyID = 4, 
                    Departure="Sofia", 
                    Destination = "Burgas", 
                    DepartureTime = DateTime.Now.AddDays(1),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(6),
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