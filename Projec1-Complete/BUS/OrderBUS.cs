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
        public Person GetPersonByOrderId(int orderId)
        {
            return orderDAL.GetPersonByOrderId(orderId);
        }
        public List<Order> GetOrderList(bool status)
        {
            return orderDAL.GetOrderList(status);
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
        
       
        public bool PersonIDExists(int personID)
        {
            return orderDAL.PersonIDExists(personID);
        }
        public List<ProductAndOrderInfo> GetOrdersByPersonId2(int personId, int orderid)
        {
            return orderDAL.GetOrdersByPersonId2(personId, orderid);
        }
        public Order ReturnOrderIdByPersonId(int personId, bool status)
        {
          return orderDAL.ReturnOrderIdByPersonId(personId,status);
        }
      public void UpdateDiscount(int id, int discount)
        {
           orderDAL.UpdateDiscount(id, discount);
        }
        public void UpdateStatus(int id)
        {
            orderDAL.UpdateStatus(id);
        }
        public void CreateNewOrder(int id)
        {
            orderDAL.CreateNewOrder(id);
        }
        public List<Order> GetListOrderBySearch(string orderId)
        {
            return orderDAL.GetOrderListBySearch(orderId);
        }


    }
}
