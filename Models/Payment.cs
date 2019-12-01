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
    
    public class Payment {
        
        private readonly Bank Bank = new Bank();
        

        // A dummy payment model
        public bool Pay(string Name, long CardNumber, int ExpirationYear, int ExpirationMonth, int CVV) 
        {
            foreach (Account Account in Bank.GetAccounts()) {
                if (Name.Equals(Account.Name) 
                    && CardNumber == Account.CardNumber
                    && ExpirationYear == Account.ExpirationYear
                    && ExpirationMonth == Account.ExpirationMonth
                    && CVV == Account.CVV
                ) 
                {
                    // Send request to the Bank API to check if the information is valid...

                    return true;
                }
            }

            return false;
        }

        public void Refund(string UserId, int BookingID) 
        {
            // ...
        }
    }
}