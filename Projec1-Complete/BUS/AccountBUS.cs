using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class AccountBUS
    {
        public AccountDAL accountDAL;
        public AccountBUS()
        {
            accountDAL = new AccountDAL();
        }
        public bool CheckAccount(string username, string password)
        {
            return accountDAL.CheckAccount(username, password);
        }
        public dynamic GetAccountID(int id)
        {
            return accountDAL.GetAccountById(id);
        }
        public string GetPersonById(int id)
        {
            return accountDAL.GetPersonById(id);
        }
        public string CheckType(string username)
        {
            return accountDAL.CheckStatusByUsernameOrEmailOrPhone(username);
        }
        public Employees GetInfoEmployees (int id)
        {
            return accountDAL.GetInfoEmployees(id);
        }
        public int GetIdPersonByUsername(string username)
        {
            return accountDAL.GetIdPersonByUsernameOrEmailOrPhone(username);
        }
        public void UpdatePassword(string password, string email)
        {
            accountDAL.UpdatePassword(password, email);
        }
    }
}
