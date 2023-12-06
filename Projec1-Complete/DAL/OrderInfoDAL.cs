using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class OrderInfoDAL
    {
        private ASMProject1Entities db;
        public OrderInfoDAL()
        {
            db = new ASMProject1Entities();
        }
        public List<OrderInfo> GetProductByID(string productID)
        {
            var order = db.OrderInfoes.Where( p => p.ProductID == productID).ToList();
            return order;
        }
    }
}
