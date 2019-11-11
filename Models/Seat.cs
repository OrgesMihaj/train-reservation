using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}