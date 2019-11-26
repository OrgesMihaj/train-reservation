using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TrainReservation.Data;
using System.Threading.Tasks;

namespace TrainReservation.Models {
    
    public class Payment {
        
        // A dummy payment model
        public bool pay() 
        {
            return true;
        }
    }
}