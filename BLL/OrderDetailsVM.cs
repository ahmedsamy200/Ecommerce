using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class OrderDetailsVM
    {
        public int orderDetailsID { get; set; }
        public Nullable<int> productID { get; set; }
        public string productName { get; set; }
        public bool? status { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> quntity { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<int> orderID { get; set; }
    }
}
