using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainReservation.Models
{
    public class Booking {
        public int BookingID { get; set; }

        public int UserID { get; set; } 
        public int JourneyID { get; set; } 
    }
}