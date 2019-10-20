using System;
using System.Linq;
using TrainReservation.Data;
using TrainReservation.Models;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Identity;

public static class AuthInitializer
{
    public static void Initialize(UserManager<IdentityUser> userManager)
    {
        string password = "P@ssword1";

        // list of admin users
        string[] users = new string[]
        {
            "onm170@aubg.edu",
            "lns170@aubg.edu",
            "bnl160@aubg.edu"
        };

        foreach (var user in users) {
            // check if user already exists
            if (userManager.FindByEmailAsync(user).Result == null)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = user,
                    Email = user
                };

                // add new user
                IdentityResult result = userManager.CreateAsync(identityUser, password).Result;

                if (result.Succeeded)
                {
                    // assign admin role to user
                    userManager.AddToRoleAsync(identityUser, "Admin").Wait();
                }
            }       
        }

        
    }   
}