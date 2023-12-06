using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class PersonDAL
    {
        private ASMProject1Entities db;
        public PersonDAL()
        {
            db = new ASMProject1Entities();
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

            db.People.Add(customer);
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
        public List<Person> SearchCustomer(string search)
        {
            var cus = db.People.Where(c => c.PersonID.ToString().Contains(search) || c.PersonName.ToString().Contains(search) || c.Address.ToString().Contains(search) || c.Phone.ToString().Contains(search) || c.Email.ToString().Contains(search)).ToList();
            return cus;

        }
    }
}
