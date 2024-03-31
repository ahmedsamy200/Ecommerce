using BLL;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IOrderDetailsRepository
    {
        IEnumerable<OrderDetailsVM> GetOrderDetails(int orderID);
    }
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        EcommerceEntities db = new EcommerceEntities();    

        public IEnumerable<OrderDetailsVM> GetOrderDetails(int orderID)
        {
            try
            {
                var result = db.OrderDetails.Where(x => x.orderID == orderID).ToList();
                var OrderVM = ConvertOrderDetailsToVM(result);
                return OrderVM;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<OrderDetailsVM> ConvertOrderDetailsToVM(IEnumerable<OrderDetail> orderDetail)
        {
            List<OrderDetailsVM> orderDetailsVM = new List<OrderDetailsVM>();
            foreach (var item in orderDetail)
            {
                OrderDetailsVM orderVM = new OrderDetailsVM();
                orderVM.productName = item.Product.prodName;
                orderVM.price = item.Product.price;
                orderVM.quntity = item.quntity;
                orderVM.total = item.total;
                orderDetailsVM.Add(orderVM);
            }
            return orderDetailsVM;
        }
    }
}
