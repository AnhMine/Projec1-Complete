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
        public string CheckType(string username)
        {
            return accountDAL.CheckStatus(username);
        }
    }
}
