using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TrainReservation.Data;
using System.Threading.Tasks;
using TrainReservation.Models;

namespace TrainReservation.Models {
    
    public class Bank {

        private Account[] Accounts = new Account[] {
            new Account { Name = "Orges Mihaj",   CardNumber = 4888351996399263, ExpirationYear = 22, ExpirationMonth = 6, CVV = 581  },
            new Account { Name = "Lazaron Shyta", CardNumber = 4888351996399263, ExpirationYear = 22, ExpirationMonth = 6, CVV = 581  },
            new Account { Name = "Besar Limani",  CardNumber = 4888351996399263, ExpirationYear = 22, ExpirationMonth = 6, CVV = 581  }
        };    

        // Retrieve a list of accounts stored in the "DB"
        public Account[] GetAccounts() {
            return Accounts;
        }


    }
}