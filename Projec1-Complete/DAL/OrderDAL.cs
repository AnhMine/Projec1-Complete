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
        public List<Order> GetOrderList(bool status)
        {
           var data = db.Orders.Where(o => o.Status == status);
            return data.ToList();
        }
        public Person GetPersonByOrderId(int orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderID == orderId);

            if (order != null)
            {
                var person = db.People.FirstOrDefault(p => p.PersonID == order.PersonID);

                return person;
            }
            else
            {
                // Đơn hàng không tồn tại, có thể xử lý tùy thuộc vào yêu cầu của bạn
                return null;
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
            if (personId !=0)
            {
                var order = db.Orders.FirstOrDefault(o => o.OrderID == personId);
                return (bool)order.Status;
            }
            else { return false; }
       
          
        }
        public decimal GetTotalAmount(int id)
        {
            
            var totalAmount = db.OrderInfoes
                .Where(oi => oi.OrderID == id)
                .Sum(oi => oi.Quantity * oi.Product.PriceSell);
            var status = db.Orders.FirstOrDefault(s => s.OrderID == id);
            if(totalAmount ==  null)
            {
                return 0;
            }    
            else
            {
                if (status != null)
                {
                    if (status.Status == false)
                    {

                        return (decimal)(totalAmount - status.Discount);

                    }
                    else
                    {
                        return 0;

                    }

                }
                else { return 0; }

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
                          TotalPrice = (decimal)(combined.OrderAndInfo.OrderInfo.Quantity * product.PriceSell),
                          DisplayStatus = combined.OrderAndInfo.Order.Status == true ? "Thanh Toán" : "Chưa Thanh Toán"
                      })
                .Where(item => item.order.PersonID == personId)
                .ToList();

            var result = ordersAndCustomers.Select(item =>
            {
                if (item.order != null && item.person != null && item.Product != null)
                {
                    var totalAmount = CalculateTotalAmount(new List<OrderInfo> { item.orderInfo }, new List<Product> { item.Product }, (int)item.order?.Discount);
                    return new ProductAndOrderInfo
                    {
                        order = item.order,
                        person = item.person,
                        Product = item.Product,
                        orderInfo = item.orderInfo,
                        DisplayStatus = item.DisplayStatus,
                        TotalPrice = item.TotalPrice,
                        TotalAmount = totalAmount
                        
                    };
                }
                return null; 
            }).Where(item => item != null)
.ToList();


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




        public decimal CalculateTotalAmount(List<OrderInfo> orderInfos, List<Product> products, decimal discount)
        {
            decimal totalAmount = 0;

            foreach (var orderInfo in orderInfos)
            {
                var product = products.FirstOrDefault(p => p.ProductID == orderInfo.ProductID);

                if (product != null)
                {
                    totalAmount += (decimal)(orderInfo.Quantity ?? 0) * (decimal)(product.PriceSell ?? 0);
                }
            }

            // Áp dụng chiết khấu
            totalAmount -= totalAmount * discount / 100;

            return totalAmount;
        }

        public bool PersonIDExists(int personID)
        {
            return db.Orders.Any(p => p.PersonID == personID );
        }
       public void UpdateDiscount (int orderID, int discount)
        {
            Order update = db.Orders.FirstOrDefault(p=> p.OrderID == orderID);
            if (update != null)
            {
                update.Discount = discount;
                db.SaveChanges();
            }

        }
        public void UpdateStatus(int orderID)
        {
            Order order = db.Orders.FirstOrDefault(o=> o.OrderID == orderID);
            if(order != null)
            {
                order.Status = true;
                db.SaveChanges();
            }    
          
        }
        public void CreateNewOrder(int id)
        {
            var maxid = db.Orders.Max(o => o.OrderID);
            var order = db.Orders.FirstOrDefault(p => p.PersonID == id);
            if(order.Status == true)
            {
                Order order2 = new Order();
                {
                    order.OrderID = maxid++;
                    order.Status = false;
                    order.AccountID = 1;
                    order.Discount = 0;
                    order.OrderDate = DateTime.Now;
                    order.PersonID = id;
                    db.SaveChanges();

                };
            }    
        }
        public Order ReturnOrderIdByPersonId(int personId)
        {
           var order = db.Orders.FirstOrDefault(p=> p.PersonID == personId);
            return order;
          
        }
       

    }
}
