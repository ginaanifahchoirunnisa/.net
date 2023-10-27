using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class Food
    {
        public int food_id { get; set; }
        public string food_name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
