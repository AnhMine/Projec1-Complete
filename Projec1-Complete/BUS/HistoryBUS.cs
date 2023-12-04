using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class HistoryBUS
    {
        HistoryDAL historyDAL;
        public HistoryBUS()
        {
            historyDAL = new HistoryDAL();
        }
        public List<History> GetHistories()
        {
            return historyDAL.GetHistories();
        }
        public void DelHistory(int id)
        {
            historyDAL.DelHistory(id);
        }
    }
}
