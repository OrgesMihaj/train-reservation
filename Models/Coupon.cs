using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TrainReservation.Data;
using System.Threading.Tasks;

namespace TrainReservation.Models
{
    public class Coupon 
    {
        public int CouponID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Percentage { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public string Code { get; set; }

        // Generate coupon code by shuffling coupon name and adding a random number
        public string GenerateCouponCode(string CouponName) {
            Random random = new Random();

            // Create new string from the reordered char array based on the coupon name. 
            string shuffleStringLetters = new string(CouponName.Replace(" ", String.Empty).ToCharArray().OrderBy(s =>  
                (random.Next(2) % 2) == 0
            ).ToArray());

            // Create a random four-digit number. 
            int randomFourDigitsNumber = random.Next(1000, 9999);   

            return shuffleStringLetters + randomFourDigitsNumber;
        }

        // Check if the user is using a valid coupon and, if so, 
        // apply it to the price.
        public decimal ApplyCoupon(ApplicationDbContext _context, decimal Cost, string CouponCode) {
            
            // Search for an existing coupon which as the same code as the one provided 
            // by the user. 
            Coupon Coupon = _context.Coupons.Single(c => c.Code == CouponCode);

            // if you find one,
            if (Coupon != null) {
                    
                decimal percentageDiscount = 0;

                // then 
                
                // check if there is a discount in dollar (fixed amount)
                if (Coupon.Amount.Equals(null)) {
                    Coupon.Amount = 0;
                }

                // check if there is a discount in percentage 
                if (!Coupon.Percentage.Equals(null)) {
                    percentageDiscount = (Coupon.Percentage / 100) * Cost;
                }

                // apply the disxount 
                Cost = Cost - (percentageDiscount + Coupon.Amount);

                // It may happen that the discount surpasses the cost.
                // So, the user has a free ticket. :) 
                if (Cost < 0) Cost = 0;
            }

            return Cost;
        }



    }
}