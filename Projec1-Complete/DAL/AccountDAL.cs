using System;
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
        public bool CheckStatus(string username)
        {
            var acc = db.Accounts.FirstOrDefault( a => a.UserName == username);
            return acc.Type;
        }
    }
}
