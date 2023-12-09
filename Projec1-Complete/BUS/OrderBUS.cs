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
        public bool GetStatus(int id)
        {
            return orderDAL.GetStatus(id);
        }
        public int GetOrder(int id)
        { 
            return orderDAL.GetOrder(id);
        }
        public decimal GetTotalAmount(int id)
        {
            return orderDAL.GetTotalAmount(id);
        }
        public void AddProductToOrder(Order order, OrderInfo orderInfo, short quantityOrder)
        
        {
            orderDAL.AddProductToOrder(order, orderInfo, quantityOrder);
        }
        public bool PersonIDExists(int personID)
        {
            return orderDAL.PersonIDExists(personID);
        }
     }
}
