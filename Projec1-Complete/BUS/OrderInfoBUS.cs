using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class OrderInfoBUS
    {
        private OrderInfoDAL _orderInfoDAL;
        public OrderInfoBUS()
        {
            _orderInfoDAL = new OrderInfoDAL();
        }
        public OrderInfo GetOrderInfoByOrderAndProduct(int orderID, string productId)
        {
            return _orderInfoDAL.GetOrderInfoByOrderAndProduct(orderID, productId);
        }
        public void UpdateOrderInfo(OrderInfo orderInfo)
        {
             _orderInfoDAL.UpdateOrderInfo(orderInfo);
        }
        public void RemoveProductFromOrder(OrderInfo orderInfo)
        {
            _orderInfoDAL.RemoveProductFromOrder(orderInfo);
        }
        public void DelProduct(string productId, int orderId)
        {
            _orderInfoDAL.DelProduct(productId, orderId);
        }
    }
}