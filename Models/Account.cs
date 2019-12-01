using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TrainReservation.Data;
using System.Threading.Tasks;

namespace TrainReservation.Models {

    public class Account {
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [RegularExpression(@"^4[0-9]{12}(?:[0-9]{3})?$")]
        public long CardNumber { get; set; }

        [Required]
        [Range(1, 12)]
        public int ExpirationMonth { get; set; }

        [Required]
        public int ExpirationYear { get; set; }
        
        [Required]
        public int CVV { get; set; }
    }
}