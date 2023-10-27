using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.DTO.Response
{
    public class FoodResponse
    {
        public int food_id { get; set; }
        public string food_name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }

    }
}
