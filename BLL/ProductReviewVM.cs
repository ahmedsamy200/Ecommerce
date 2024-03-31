using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class ProductReviewVM
    {
        public int reviewID { get; set; }
        public Nullable<int> productID { get; set; }
        public Nullable<int> userID { get; set; }
        public string customerName { get; set; }
        public string customerImage { get; set; }

        public string commentDate { get; set; }
        public string comment { get; set; }
        public int countComments { get; set; }

        public Nullable<byte> oneTotal { get; set; }
        public Nullable<byte> twoTotal { get; set; }
        public Nullable<byte> threeTotal { get; set; }
        public Nullable<byte> fourTotal { get; set; }
        public Nullable<byte> fiveTotal { get; set; }
      
        public Nullable<byte> rating { get; set; }
        public Nullable<double> starOneAvg { get; set; }
        public Nullable<double> starTwoAvg { get; set; }
        public Nullable<double> starThreeAvg { get; set; }
        public Nullable<double> starFourAvg { get; set; }
        public Nullable<double> starFiveAvg { get; set; }

        public double avgStar { get; set; }
        public Nullable<double> finalResult { get; set; }

    }
}
