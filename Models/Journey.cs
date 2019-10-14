using System;
using System.Collections.Generic;

namespace TrainReservation.Models
{
    public class Journey {
        public int JourneyID { get; set; }
        public string DepartsFrom { get; set; }

        public string Destination { get; set; }

        public DateTime DepartsAt { get; set; }

        public DateTime ArrivesAt { get; set; }

        public int TrianID { get; set; }

        public Train Train { get; set; }
    }
}