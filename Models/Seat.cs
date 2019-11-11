using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TrainReservation.Data;

namespace TrainReservation.Models {
    
    public class Seat {

        public int SeatID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        public int Number { get; set; }

        public virtual Booking Booking { get; set; }

        // Reserve seat(s) for a given booking
        public void reserveSeats(ApplicationDbContext _context, Booking booking, string UserID, string SeatsRequested) {
            
            // SeatsRequested is given in the format: "{n},{n},{n},"
            // {n} consists of an integers (INT)
            // Seperate the string into substrings of {n}
            string[] seats = SeatsRequested.Split(',');
            
            foreach (string seat in seats) {

                // check if the substring {n} is convertible into INT 
                if (int.TryParse(seat, out int seatNumber)) {
                    
                    Seat s = new Seat();

                    s.BookingID = booking.BookingID;
                    s.UserID = UserID;
                    s.Number = seatNumber;

                    _context.Seats.Add(s);
                }
            }
            /* </foreach> */
            
        }

    }
}