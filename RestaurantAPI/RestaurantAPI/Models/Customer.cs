using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class Customer
    {
      
        public int customer_id { get; set; }


        public String first_name { get; set; }

        public String last_name { get; set; }


        public String email { get; set; }

        public String phone_number { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
