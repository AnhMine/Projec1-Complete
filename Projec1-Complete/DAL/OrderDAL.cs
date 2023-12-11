using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projec1_Complete.DAL
{
    public class OrderDAL
    {
        private ASMProject1Entities db;
        public OrderDAL()
        {
            db = new ASMProject1Entities();
        }
        public void AddProductToOrder(Order order, OrderInfo orderInfo)
        {
            string productId = orderInfo.ProductID;
            Product prd = db.Products.FirstOrDefault(x => x.ProductID == productId);
            if(prd != null)
            {
                db.OrderInfoes.Add(orderInfo);
                db.SaveChanges();
            }    
         
        }
        public int GetOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(o => o.PersonID == id);

            if (order != null)
            {
                return order.OrderID;
            }

            // Trong trường hợp không tìm thấy đơn hàng
            return -1; // hoặc giá trị mặc định phù hợp với yêu cầu của bạn
        }

        public bool GetStatus(int personId)
        {
            var order = db.Orders.FirstOrDefault(o => o.PersonID == personId);
            return order != null && order.Status != null && order.Status.Value;
        }
        public decimal GetTotalAmount(int id)
        {
            var totalAmount = db.OrderInfoes
                .Where(oi => oi.OrderID == id)
                .Sum(oi => oi.Quantity * oi.Product.PriceSell);
            if(totalAmount  == null)
            {
                return 0;
            }
            else
            {
                return (decimal)totalAmount;
            }
        }
        public List<ProductAndOrderInfo> GetOrdersByPersonId2(int personId)
        {
            var ordersAndCustomers = db.Orders
                .Join(db.OrderInfoes, order => order.OrderID, orderinfo => orderinfo.OrderID,
                      (order, orderinfo) => new { Order = order, OrderInfo = orderinfo })
                .Join(db.People, combined => combined.Order.PersonID, person => person.PersonID,
                      (combined, person) => new { OrderAndInfo = combined, Customer = person })
                .Join(db.Products, combined => combined.OrderAndInfo.OrderInfo.ProductID, product => product.ProductID,
                      (combined, product) => new ProductAndOrderInfo
                      {
                          order = combined.OrderAndInfo.Order,
                          person = combined.Customer,
                          Product = product,
                          orderInfo = combined.OrderAndInfo.OrderInfo,
                          CategoryID = (int)product.CategoryID,
                          DisplayStatus = combined.OrderAndInfo.Order.Status == true ? "Thanh Toán" : "Chưa Thanh Toán"
                      })
                .Where(item => item.order.PersonID == personId)
                .ToList();

            var result = ordersAndCustomers.Select(item => new ProductAndOrderInfo
            {
                order = item.order,
                person = item.person,
                Product = item.Product,
                orderInfo = item.orderInfo,
                CategoryID = item.CategoryID,
                DisplayStatus = item.DisplayStatus,
                TotalAmount = CalculateTotalAmount((int)item.orderInfo.Quantity, (decimal)item.Product.PriceSell, (int)item.order.Discount)
            }).ToList();

            return result;
        }
        public List<ProductAndOrderInfo> GetProductsByPersonId(int personId)
        {
            var products = db.Products.ToList();
            var orderInfo = (from oi in db.OrderInfoes
                             join o in db.Orders on oi.OrderID equals o.OrderID
                             where o.PersonID == personId
                             select new
                             {
                                 oi.ProductID,
                                 oi.Quantity,
                                 oi.OrderID,
                             }).ToList();

            var orderInfoDict = orderInfo.ToDictionary(x => x.ProductID, x => x.Quantity);

            var productsWithOrderInfo = products.Select(p =>
            {
                var quantity = (byte)(orderInfoDict.ContainsKey(p.ProductID) ? orderInfoDict[p.ProductID] : 0);
                var orderInfoItem = orderInfo.FirstOrDefault(oi => oi.ProductID == p.ProductID);

                return new ProductAndOrderInfo
                {
                    Product = p,
                    orderInfo = orderInfoItem != null ? new OrderInfo { Quantity = orderInfoItem.Quantity } : null
                };
            }).ToList();


            return productsWithOrderInfo;
        }




        private decimal CalculateTotalAmount(int quantity, decimal priceSell, int discount)
        {
            decimal discountedPrice = priceSell - (priceSell * discount / 100);
            return quantity * discountedPrice;
        }
        public bool PersonIDExists(int personID)
        {
            return db.Orders.Any(p => p.PersonID == personID );
        }

        public int ReturnOrderIdByPersonId(int personId)
        {
            var order = db.Orders.FirstOrDefault(o => o.PersonID == personId);
            var maxOrderId = db.Orders.Max(o => (int?)o.OrderID) ?? 0;
            int newOrderId = maxOrderId + 1;
     
            if (order != null)
            {
                return order.OrderID;
            }
            else
            {
                Order newOrder = new Order
                {
                    OrderID = newOrderId,
                    PersonID = personId,
                    AccountID = 1,
                    OrderDate = DateTime.Now,
                    Status = false,
                    Discount = 0,
                };

                db.Orders.Add(newOrder);  
                db.SaveChanges();  

                return newOrder.OrderID;
            }
        }


    }
}
