using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class HistoryDAL
    {
        private ASMProject1Entities db;
        public HistoryDAL()
        {
            db = new ASMProject1Entities();
        }
        public List<History> GetHistories()
        {
            return db.Histories.ToList();
        }
        public void DelHistory(int id)
        {
            var his = db.Histories.FirstOrDefault(h => h.HistoryID == id);
            if (his != null)
            {
                db.Histories.Remove(his);
                db.SaveChanges();
            }
        }
    }
}
