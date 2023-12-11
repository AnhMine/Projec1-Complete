using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Projec1_Complete.DAL
{
    public class AccountDAL
    {
        private ASMProject1Entities db;
        public AccountDAL()
        {
            db = new ASMProject1Entities();
        }
        public bool CheckAccount(string username, string password)
        {
            var acc = db.People
       .Join(db.Accounts,
           p => p.PersonID,
           a => a.PersonID,
           (p, a) => new { Person = p, Account = a })
       .FirstOrDefault(pa =>
           (pa.Account.UserName == username || pa.Person.Email == username || pa.Person.Phone == username)
           && pa.Account.Password == password);

            return acc != null;
        }
        public dynamic GetAccountById(int id)
        {

            var acc = db.Accounts.FirstOrDefault(a => a.AccountID == id);

            return acc;
        }
        public int GetIdPersonByUsernameOrEmailOrPhone(string usernameOrEmailOrPhone)
        {
            var personId = db.People
                .Where(p => p.Email == usernameOrEmailOrPhone || p.Phone == usernameOrEmailOrPhone ||
                            db.Accounts.Any(acc => acc.UserName == usernameOrEmailOrPhone && acc.PersonID == p.PersonID))
                .Select(p => p.PersonID)
                .FirstOrDefault();

            return (int)personId; // Trả về -1 nếu không có người dùng nào khớp.
        }


        public string GetPersonById(int id)
        {
            var person = db.People.FirstOrDefault(a => a.PersonID == id);
            return person.Type;
        }
        public string CheckStatusByUsernameOrEmailOrPhone(string usernameOrEmailOrPhone)
        {
            var personType = db.Accounts
                .Where(acc => acc.UserName == usernameOrEmailOrPhone ||
                              db.People.Any(p => (p.Email == usernameOrEmailOrPhone || p.Phone == usernameOrEmailOrPhone) && p.PersonID == acc.PersonID))
                .Join(db.People, acc => acc.PersonID, person => person.PersonID, (acc, person) => person.Type)
                .FirstOrDefault();

            return personType ?? "Hacker";
        }

        public Employees GetInfoEmployees(int id)
        {
            var info = (from person in db.People
                        join account in db.Accounts on person.PersonID equals account.PersonID
                        where person.PersonID == id
                        select new Employees
                        {
                            person = person,
                            account = account
                           
                        }).FirstOrDefault();

            return info;
        }
        public void UpdatePassword(string password, string email)
        {
            var person = db.People.FirstOrDefault(p => p.Email == email);

            if (person != null)
            {
                var account = db.Accounts.FirstOrDefault(a => a.PersonID == person.PersonID);

                if (account != null)
                {
                    account.Password = password;

                    db.SaveChanges();
                }
            }
        }
       



    }
}
