using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

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

        public void UpdateOrderInfo2(OrderInfo orderInfo)
        {
            var prd = db.Products.FirstOrDefault(p => p.ProductID == orderInfo.ProductID);
            var  info = db.OrderInfoes.FirstOrDefault(o => o.OrderID == orderInfo.OrderID && o.ProductID == orderInfo.ProductID);
            if(info != null)
            {
                info.Quantity--;
                prd.Quantity++;
            }    
        }
        public void RemoveProductFromOrder(OrderInfo orderInfo)
        {
            var existingOrderInfo = db.OrderInfoes.FirstOrDefault(o => o.OrderID == orderInfo.OrderID && o.ProductID == orderInfo.ProductID);
            var product = db.Products.FirstOrDefault(p => p.ProductID == orderInfo.ProductID);

            if (existingOrderInfo != null && product != null)
            {
                // Giảm số lượng đặt hàng
                existingOrderInfo.Quantity--;

                // Tăng số lượng trong kho
                product.Quantity++;

                if (existingOrderInfo.Quantity == 0)
                {
                    // Nếu số lượng đặt hàng giảm về 0, xóa OrderInfo khỏi ngữ cảnh
                    db.OrderInfoes.Remove(existingOrderInfo);
                }

                db.SaveChanges();  // Lưu các thay đổi ra cơ sở dữ liệu
            }
        }

        public void UpdateOrderInfo(OrderInfo orderInfo)
{
    var existingOrderInfo = db.OrderInfoes.FirstOrDefault(o => o.OrderID == orderInfo.OrderID && o.ProductID == orderInfo.ProductID);
    var product = db.Products.FirstOrDefault(p => p.ProductID == orderInfo.ProductID);

    if (existingOrderInfo != null)
    {
        if (product != null && product.Quantity > 0)
        {
            existingOrderInfo.Quantity++;
            product.Quantity--;
        }
        else
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Hết Hàng Trong Kho. Bạn Vẫn Muốn Tiếp Tục?", "Thông Báo", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }
        }
    }
    else
    {
        if (product != null && product.Quantity > 0)
        {
            OrderInfo newOrderInfo = new OrderInfo
            {
                OrderID = orderInfo.OrderID,
                ProductID = orderInfo.ProductID,
                Quantity = orderInfo.Quantity
            };
            product.Quantity--;

            db.OrderInfoes.Add(newOrderInfo);  
        }
        else
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Hết Hàng Trong Kho. Bạn Vẫn Muốn Tiếp Tục?", "Thông Báo", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }
        }
    }

    db.SaveChanges();  
}

    }
}
