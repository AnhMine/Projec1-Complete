using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projec1_Complete.DAL
{
    public class PersonDAL
    {
        private ASMProject1Entities db;
        public PersonDAL()
        {
            db = new ASMProject1Entities();
        }
        public List<Person> GetPersonById(int id)
        {
            var person = db.People.Where( p=> p.PersonID == id);
            return  person.ToList();

        }
        public List<Person> SearchPersonById(string search)
        {
            var peopleQuery = db.People.Where(p => p.PersonName.Contains(search) || p.PersonID.ToString().Contains(search));

            return peopleQuery.ToList();
        }

        public List<Person> GetListCustomers()
        {
            return db.People.ToList();


        }
        public int GetMaxCustomerID()
        {
            int maxID = db.People.Any() ? db.People.Max(m => m.PersonID) : 0;
            return maxID;
        }

        public void AddCustomer(Person customer)
        {
            int maxID = GetMaxCustomerID();
            int newID = maxID + 1;
            customer.PersonID = newID;
            
            Order order = new Order();
            {
                order.AccountID = 1;
                order.OrderDate = DateTime.Now;
                order.Status = false;
                order.Discount = 0;
                order.PersonID = newID;
                

            }
            db.People.Add(customer);
            db.Orders.Add(order);
            db.SaveChanges();
        }
        public void RemoveCustomer(int id)
        {
            var cus = db.People.FirstOrDefault(c => c.PersonID == id);
            db.People.Remove(cus);
            db.SaveChanges();
        }
        public void UpdateCustomer(Person customer)
        {
            var cus = db.People.FirstOrDefault(c => c.PersonID == customer.PersonID);
            if (cus != null)
            {
                cus.PersonName = customer.PersonName;
                cus.Address = customer.Address;
                cus.Phone = customer.Phone;
                cus.Email = customer.Email;
                db.SaveChanges();
            }
        }
        public Person GetPersonBySearchString(string searchString,int orderid)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderID == orderid);

            if (order != null)
            {
                // Tìm người với PersonID tương ứng trong đơn hàng
                var person = db.People.FirstOrDefault(p => p.PersonID == order.PersonID && p.PersonName.Contains(searchString));

                return person;
            }
            else
            {
                // Đơn hàng không tồn tại hoặc không có người phù hợp, có thể xử lý tùy thuộc vào yêu cầu của bạn
                return null;
            }
        }





        public void UpdatePersonAndAccount(Employees employees)
        {
            if (employees == null || employees.person == null || employees.account == null)
            {
                
                return;
            }
            var existingPerson = db.People.FirstOrDefault(p => p.PersonID == employees.person.PersonID);

            if (existingPerson != null)
            {
                existingPerson.PersonImage = employees.person.PersonImage;
                existingPerson.PersonName = employees.person.PersonName;
                existingPerson.Address = employees.person.Address;
                existingPerson.Phone = employees.person.Phone;
                existingPerson.Email = employees.person.Email;
                db.SaveChanges();
            }

            var existingAccount = db.Accounts.FirstOrDefault(a => a.PersonID == employees.account.PersonID);

            if (existingAccount != null)
            {
                existingAccount.Password = employees.account.Password;

                db.SaveChanges();
            }
        }
        public List<Person> GetInfoPerson(int id)
        {
            var people = db.People.Where(p=> p.PersonID==id).ToList();
            return people;
        }
        public Person GetPerson(int id)
        {
            var people = db.People.FirstOrDefault(p=>p.PersonID==id);
            return people;
        }


    }
}
