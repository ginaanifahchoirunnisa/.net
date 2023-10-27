using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.DTO.NewFolder2
{
    public class CreateTransactionDTO
    {
        public int food_id { get; set; }
        public int customer_id { get; set; }
        public DateTime transaction_date { get; set; }
        public decimal amount { get; set; }
    }
}
