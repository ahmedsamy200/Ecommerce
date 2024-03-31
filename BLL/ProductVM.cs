using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class ProductVM
    {
        public int productID { get; set; }
        public string prodName { get; set; }        
        public string CategoryName { get; set; }
        public string prodDescription { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<int> inStock { get; set; }
        public string img { get; set; }
        public Nullable<bool> isFeature { get; set; }
        public Nullable<bool> isFavorate { get; set; }
        public Nullable<int> categoryID { get; set; }
        public Nullable<int> rank { get; set; }
        public Nullable<double> totalStars { get; set; }
        public Nullable<int> numberOfReviews { get; set; }

        public Nullable<byte> oneTotal { get; set; }
        public Nullable<byte> twoTotal { get; set; }
        public Nullable<byte> threeTotal { get; set; }
        public Nullable<byte> fourTotal { get; set; }
        public Nullable<byte> fiveTotal { get; set; }
        public double avgStar { get; set; }

        public Nullable<double> starOneAvg { get; set; }
        public Nullable<double> starTwoAvg { get; set; }
        public Nullable<double> starThreeAvg { get; set; }
        public Nullable<double> starFourAvg { get; set; }
        public Nullable<double> starFiveAvg { get; set; }

        public int countComments { get; set; }

    }
}
