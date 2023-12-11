﻿using Projec1_Complete.DAL;
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
        public void AddProductToOrder(Order order, OrderInfo orderInfo)
        
        {
            orderDAL.AddProductToOrder(order, orderInfo);
        }
        public bool PersonIDExists(int personID)
        {
            return orderDAL.PersonIDExists(personID);
        }
        public List<ProductAndOrderInfo> GetOrdersByPersonId2(int personId)
        {
            return orderDAL.GetOrdersByPersonId2(personId);
        }
        public int ReturnOrderIdByPersonId(int personId)
        {
          return orderDAL.ReturnOrderIdByPersonId(personId);
        }


    }
}
