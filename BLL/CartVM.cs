using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class CartVM
    {
        public int cartID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> productID { get; set; }
        public string prodImg { get; set; }
        public string prodName { get; set; }
        public string CategoryName { get; set; }
        public string prodDescription { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<bool> savedForLater { get; set; }
        public Nullable<bool> savedInFavorate { get; set; }
        public Nullable<System.DateTime> timeAdded { get; set; }
    }
}
