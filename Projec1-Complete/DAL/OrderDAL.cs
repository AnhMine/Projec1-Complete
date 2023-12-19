using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
           var data2 = db.Orders.Where(o => o.Status == status);
            return data2.ToList();
        }
        public List<Order> GetOrderListBySearch(string search)
        {
            var data = db.Orders
                .Where(o =>
                    search.Length > 0 && (
                        o.PersonID.ToString().Contains(search) ||
                        o.Person.PersonName.ToLower().Contains(search.ToLower())
                    ))
                .ToList();

            var data2 = db.Orders
                .Where(o => o.Status.ToString() == search)
                .ToList();

            var result = data
                .OrderByDescending(o => o.PersonID.ToString().Contains(search))
                .Concat(data2)
                .ToList();

            return result;
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
                

                        return (decimal)(totalAmount - status.Discount);

                  
                }
                else { return 0; }

            }


        }
       
        public List<ProductAndOrderInfo> GetOrdersByPersonId2(int personId, int orderid)
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
                .Where(item => item.order.PersonID == personId && item.order.OrderID == orderid)
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

        public Order ReturnOrderIdByPersonId(int personId,bool status)
        {
            var order = db.Orders.FirstOrDefault(p => p.PersonID == personId && p.Status == status );
            return order;

          
        }



        public void CreateNewOrder(int personId)
        {
            // Lấy OrderID lớn nhất trong danh sách Orders (nếu có)
            var maxOrderId = db.Orders.Max(p => (int?)p.OrderID) ?? 0;

            // Tạo một đối tượng Order mới với OrderID tăng lên từ OrderID lớn nhất
            Order newOrder = new Order
            {
                OrderID = maxOrderId + 1,
                OrderDate = DateTime.Now,
                PersonID = personId,
                Status = false,
                AccountID = 1,
                Discount = 0
            };

            // Thêm đơn hàng mới vào DbSet và lưu thay đổi vào cơ sở dữ liệu
            db.Orders.Add(newOrder);
            db.SaveChanges();
        }
        public bool CheckOrder(int personId)
        {
            var data = db.Orders.Any(p => p.PersonID == personId && p.Status == false);
            return data;
        }


    }
}
