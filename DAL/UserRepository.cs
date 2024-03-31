using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{
    interface IUserRepository
    {
        bool AddCustomer(CustomerVM customer);
        bool UpdateCustomer(CustomerVM NewCustomer);
        bool DeletCustomer(int id);
        CustomerVM GetCustomerByID(int id);
        IEnumerable<CustomerVM> GetAllCustomers();

        Customer CheckLogin(Customer customer);
    }

   public class UserRepository:IUserRepository
    {
        EcommerceEntities db = new EcommerceEntities();

        public bool AddCustomer(CustomerVM customer)
        {
            try
            {
                var validEmail = db.Customers.Where(x => x.email == customer.email).SingleOrDefault();
                if (validEmail == null)
                {
                    Customer result = ConvertCustomerVM_ToCustomer(customer);
                    result.registrationDate = DateTime.Now;
                    db.Customers.Add(result);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }

        }

        public bool IsValidEmail(string email , int id)
        {
            try
            {
                var validEmail = db.Customers.Where(x => x.email == email && x.customerID != id).SingleOrDefault();
                if (validEmail == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
           

        }
        
        public bool UpdateCustomer(CustomerVM NewCustomer)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(x => x.customerID == NewCustomer.customerID);
                if (customer != null)
                {
                    ChangeOldCustomerValues(NewCustomer);
                    return true;
                }
                else
                    return false;
            }
            catch
            {

                return false;
            }
        }

        public bool UpdateUserImage(int customerID, string imageName)
        {
            try
            {
                Customer oldCustomer = db.Customers.FirstOrDefault(x => x.customerID == customerID);
                if (oldCustomer != null)
                {
                    oldCustomer.image = imageName;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {

                return false;
            }
        }

        public bool DeletCustomer(int id)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(x => x.customerID == id);
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<CustomerVM> GetAllCustomers()
        {
            try
            {
                IEnumerable<CustomerVM> customers = ConvertAllCustomers_ToCustomersVM();
                return customers;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public CustomerVM GetCustomerByID(int id)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(x => x.customerID == id);
                CustomerVM customerVM = ConvertCustomer_ToCustomerVM(customer);
                return customerVM;
            }
            catch (Exception)
            {

                throw;
            }
           
        }


        private IEnumerable<CustomerVM> ConvertAllCustomers_ToCustomersVM()
        {
            List<CustomerVM> customers = new List<CustomerVM>();
            foreach (var item in db.Customers)
            {
                CustomerVM obj = new CustomerVM();
                obj.firstName = item.firstName;
                obj.lastName = item.lastName;
                obj.address = item.address;
                obj.city = item.city;
                obj.state = item.state;
                obj.country = item.country;
                obj.mobile = item.mobile;
                obj.email = item.email;
                obj.password = item.password;
                obj.registrationDate = item.registrationDate;
                obj.roleID = item.roleID;
                customers.Add(obj);
            }
            return customers;
        }

        private Customer ConvertCustomerVM_ToCustomer(CustomerVM customer)
        {
            Customer obj = new Customer();
            obj.firstName = customer.firstName;
            obj.lastName = customer.lastName;
            obj.address = customer.address;
            obj.city = customer.city;
            obj.state = customer.state;
            obj.country = customer.country;
            obj.mobile = customer.mobile;
            obj.email = customer.email;
            obj.password = customer.password;
            obj.registrationDate = customer.registrationDate;
            obj.roleID = customer.roleID;
            return obj;
        }

        private CustomerVM ConvertCustomer_ToCustomerVM(Customer customer)
        {
            CustomerVM obj = new CustomerVM();
            obj.firstName = customer.firstName;
            obj.lastName = customer.lastName;
            obj.address = customer.address;
            obj.city = customer.city;
            obj.state = customer.state;
            obj.country = customer.country;
            obj.mobile = customer.mobile;
            obj.email = customer.email;
            obj.password = customer.password;
            obj.registrationDate = customer.registrationDate;
            obj.roleID = customer.roleID;
            obj.image = customer.image;
            return obj;
        }

        private void ChangeOldCustomerValues(CustomerVM NewCustomer)
        {
            Customer OldCustomer = db.Customers.FirstOrDefault(x => x.customerID == NewCustomer.customerID);
            OldCustomer.firstName = NewCustomer.firstName;
            OldCustomer.lastName = NewCustomer.lastName;
            OldCustomer.address = NewCustomer.country + "-"+ NewCustomer.city + "-" + NewCustomer.state;
            OldCustomer.city = NewCustomer.city;
            OldCustomer.state = NewCustomer.state;
            OldCustomer.country = NewCustomer.country;
            OldCustomer.mobile = NewCustomer.mobile;
            OldCustomer.email = NewCustomer.email;
            OldCustomer.roleID = NewCustomer.roleID;
            db.SaveChanges();
        }

        public Customer CheckLogin(Customer customer)
        {
            try
            {
                var obj = db.Customers.Where(x => x.email == customer.email && x.password == customer.password).FirstOrDefault();
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public bool ChangePassword(CustomerVM user)
        {
            try
            {
                Customer obj = db.Customers.FirstOrDefault(x => x.customerID == user.customerID && x.password == user.password);
                if (obj != null)
                {

                    obj.password = user.newPassword;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
           


        }
    }
}
