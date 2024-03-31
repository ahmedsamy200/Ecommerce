using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class OrderVM
    {
        public int? orderID { get; set; }
        public int? userID { get; set; }
        public bool? status { get; set; }
        public bool? IsDelivered { get; set; }
        public string shippingCity { get; set; }
        public string shippingState { get; set; }
        public string shippingAddress { get; set; }
        public string orderPhone { get; set; }
        public decimal? totalPrice { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
    }
}
