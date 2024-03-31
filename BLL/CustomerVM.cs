using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CustomerVM
    {
        public int customerID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string newPassword { get; set; }
        public string mobile { get; set; }
        public string image { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> registrationDate { get; set; }
        public Nullable<int> roleID { get; set; }
    }
}
