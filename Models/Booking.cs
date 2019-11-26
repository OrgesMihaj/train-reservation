using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainReservation.Models
{
    public class Booking {
        
        public int BookingID { get; set; }

        public string UserID { get; set; }

        public int JourneyID { get; set; }

        public int Passengers { get; set; }

        public decimal Cost { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsRefund { get; set; }

        public string CancellationMessage { get;set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Journey Journey { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}