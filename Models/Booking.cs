using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainReservation.Models
{
    public class Booking {
        
        public int BookingID { get; set; }
        public string UserID { get; set; }
        public AppUser AppUser { get; set; }

        public int JourneyID { get; set; }
        public Journey Journey { get; set; }

        public int Passengers { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}