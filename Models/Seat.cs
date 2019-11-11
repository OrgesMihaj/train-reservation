using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TrainReservation.Data;
using System.Threading.Tasks;

namespace TrainReservation.Models {
    
    public class Seat {

        public int SeatID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int JourneyID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        public int Number { get; set; }

        public virtual Journey Journey { get; set; }
        public virtual Booking Booking { get; set; }

        

        // Reserve seat(s) for a given booking
        public async void reserveSeats(
            ApplicationDbContext _context, string UserID, Booking Booking, Journey Journey, string SeatsRequested) 
        {
            
            // SeatsRequested is given in the format: "{n},{n},{n},"
            // {n} consists of an integers (INT)
            // Seperate the string into substrings of {n}
            string[] seats = SeatsRequested.Split(',');

            foreach (string SeatAsString in seats) {

                // check if the substring {n} is convertible into INT 
                if (int.TryParse(SeatAsString, out int SeatNumber))
                {
                    Seat Seat = new Seat();

                    Seat.BookingID = Booking.BookingID;
                    Seat.JourneyID = Journey.JourneyID;
                    Seat.UserID = UserID;
                    Seat.Number = SeatNumber;

                    Seat CurrentSeat = await GetCurrentSeat(_context, Seat);

                    // if the seat is not taken, then you can reserve it
                    if (CurrentSeat == null)
                    {
                        _context.Seats.Add(Seat);
                    }
                }
            }
            /* </foreach> */
            
        }

        // Check if there is an exising seat reserved 
        private static async Task<Seat> GetCurrentSeat(ApplicationDbContext _context, Seat Seat)
        {
            return await _context.Seats.FirstOrDefaultAsync(
                s => s.JourneyID == Seat.JourneyID && s.Number == Seat.Number 
            );
        }
    }
}