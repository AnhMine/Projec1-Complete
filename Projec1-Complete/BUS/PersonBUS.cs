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
  
        public List<Person> GetPersonById(int id)
        {
            return personDAL.GetPersonById(id);
        }
       
        public void UpdateEmployees(Employees employees)
        {
            personDAL.UpdatePersonAndAccount(employees);
        }
        public List<Person> GetInfoPerson(int id)
        {
            return personDAL.GetInfoPerson(id);
        }
        public Person GetPerson(int id)
        {
            return personDAL.GetPerson(id);
        }
        public Person SerachPerson(string search, int orderid)
        {
            return personDAL.GetPersonBySearchString(search,orderid);
        }
        public List<Person> SearchPersonById(string search)
        {
            return personDAL.SearchPersonById(search);
        }



    }
}
