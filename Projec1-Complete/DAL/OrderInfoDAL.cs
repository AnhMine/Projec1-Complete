﻿using System;
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
        public OrderInfo GetOrderInfoByOrderAndProduct(int orderID, string productId)
        {
            var orderInfo = db.OrderInfoes.FirstOrDefault(oi => oi.OrderID == orderID && oi.ProductID == productId);
            return orderInfo;
        }
        public void UpdateOrderInfo(OrderInfo orderInfo)
        {
            var info = db.OrderInfoes.FirstOrDefault(o => o.OrderID == orderInfo.OrderID && o.ProductID == orderInfo.ProductID);

            if (info != null)
            {
                info.Quantity++;  // Tăng giá trị Quantity cho OrderInfo hiện tại
            }
            else
            {
                OrderInfo newOrderInfo = new OrderInfo
                {
                    OrderID = orderInfo.OrderID,
                    ProductID = orderInfo.ProductID,
                    Quantity = orderInfo.Quantity
                };

                db.OrderInfoes.Add(newOrderInfo);  // Thêm OrderInfo mới vào ngữ cảnh
            }

            db.SaveChanges();  // Lưu các thay đổi ra cơ sở dữ liệu
        }

    }
}
