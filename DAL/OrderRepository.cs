using BLL;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IOrderRepository
    {
        IEnumerable<OrderVM> GetAllOreders();
        IEnumerable<OrderVM> GetUserOrders(int userID);
        bool ConfirmShipping(int orderID);
        bool ConfirmDelivered(int orderID);
        bool DeleteOrder(int orderID);
    }

    public class OrderRepository : IOrderRepository
    {
        EcommerceEntities db = new EcommerceEntities();

        public bool ConfirmShipping(int orderID)
        {
            try
            {
                var order = db.Orders.FirstOrDefault(x => x.orderID == orderID);
                order.status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ConfirmDelivered(int orderID)
        {
            try
            {
                var order = db.Orders.FirstOrDefault(x => x.orderID == orderID);
                order.IsDelivered = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteOrder(int orderID)
        {
            try
            {
                var result = db.Orders.FirstOrDefault(x => x.orderID == orderID);
                if (result != null)
                {
                    db.Orders.Remove(result);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<OrderVM> GetAllOreders()
        {
            try
            {
                IEnumerable<Order> orders = db.Orders.ToList();
                IEnumerable<OrderVM> ordersVM = ConvertOrdersToOrdersVM(orders);
                return ordersVM;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<OrderVM> GetUserOrders(int userID)
        {
            try
            {
                IEnumerable<Order> orders = db.Orders.Where(x => x.userID == userID);
                IEnumerable<OrderVM> ordersVM = ConvertOrdersToOrdersVM(orders);
                return ordersVM;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private IEnumerable<OrderVM> ConvertOrdersToOrdersVM(IEnumerable<Order> orders)
        {
            try
            {
                List<OrderVM> ordersVM = new List<OrderVM>();
                foreach (var item in orders)
                {
                    OrderVM obj = new OrderVM();
                    obj.orderID = item.orderID;
                    obj.shippingAddress = item.shippingCountry + " " + item.shippingCity + " " + item.shippingState;
                    obj.totalPrice = item.totalPrice;
                    obj.status = item.status;
                    obj.IsDelivered = item.IsDelivered;
                    obj.orderPhone = item.orderPhone;
                    ordersVM.Add(obj);
                }
                return ordersVM;
            }
            catch (Exception)
            {

                throw;
            }

           
        }

    }
}
