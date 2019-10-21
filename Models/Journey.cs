using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainReservation.Models
{
    public class Journey {
        public int JourneyID { get; set; }

        [StringLength(60, MinimumLength = 5)]
        [Required]
        public string Departure { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        
        [Required]
        public bool AllowSeatReservation { get; set; }

        [Required]
        public int TrainID { get; set; }

        // Delegation
        // Only one train can participate in a journey. 
        public virtual Train Train { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}