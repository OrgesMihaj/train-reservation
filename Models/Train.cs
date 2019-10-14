using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainReservation.Models
{
    public class Train {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrainID { get; set; }
        public string Name { get; set; }
        public bool Express { get; set; }
        public bool BookSeats { get; set; }
        public ICollection<Journey> Journeys { get; set; }
    }
}