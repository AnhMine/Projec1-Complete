using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class PersonBUS
    {
        private PersonDAL personDAL;
        public PersonBUS()
        {
            personDAL = new PersonDAL();
        }
        public List<Person> GetListCustomer()
        {
            return personDAL.GetListCustomers();
        }
        public void AddNewCustomer(Person customer)
        {
            personDAL.AddCustomer(customer);
        }
        public void RemoveCustomer(int id)
        {
            personDAL.RemoveCustomer(id);
        }
        public void UpdateCustomer(Person customer)
        {
            personDAL.UpdateCustomer(customer);
        }
        public List<Person> Search(string query)
        {
            return personDAL.SearchCustomer(query);
        }
    }
}
