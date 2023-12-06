using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class OrderBUS
    {
        public OrderDAL orderDAL;
        public OrderBUS()
        {
            orderDAL = new OrderDAL();
        }
        public List<OrdersAndCustomers> GetOrdersByPersonId(int id)
        {
            return orderDAL.GetOrdersByPersonId(id);
        }
    }
}
