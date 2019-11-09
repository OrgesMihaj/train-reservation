using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainReservation.Models
{

    public class Train {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrainID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }
        
        public bool Express { get; set; }
        public virtual ICollection<Journey> Journeys { get; set; }
    }
}