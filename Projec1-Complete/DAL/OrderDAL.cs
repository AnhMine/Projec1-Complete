using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class OrderDAL
    {
        private ASMProject1Entities db;
        public OrderDAL()
        {
            db = new ASMProject1Entities();
        }
    
        public List<OrdersAndCustomers> GetOrdersByPersonId(int personId)
        {
            var ordersAndCustomers = db.Orders
                .Join(db.OrderInfoes, order => order.OrderID, orderinfo => orderinfo.OrderID,
                      (order, orderinfo) => new { Order = order, OrderInfo = orderinfo })
                .Join(db.People, combined => combined.Order.PersonID, person => person.PersonID,
                      (combined, person) => new { OrderAndInfo = combined, Customer = person })
                .Join(db.Products, combined => combined.OrderAndInfo.OrderInfo.ProductID, product => product.ProductID,
                      (combined, product) => new OrdersAndCustomers
                      {
                          order = combined.OrderAndInfo.Order,
                          orderInfo = combined.OrderAndInfo.OrderInfo,
                          ProductName = product.ProductName,
                          PriceSell = (decimal)product.PriceSell,
                          Discount = (byte)combined.OrderAndInfo.Order.Discount
                      })
                .Where(item => item.order.PersonID == personId)
                .ToList()
                .Select(item => new OrdersAndCustomers
                {
                    order = item.order,
                    orderInfo = item.orderInfo,
                    ProductName = item.ProductName,
                    PriceSell = item.PriceSell,
                    TotalAmount = CalculateTotalAmount((int)item.orderInfo.Quantity, item.PriceSell, item.Discount)
                })
                .ToList();

            return ordersAndCustomers;
        }

        private decimal CalculateTotalAmount(int quantity, decimal priceSell, int discount)
        {
            decimal discountedPrice = priceSell - (priceSell * discount / 100);
            return quantity * discountedPrice;
        }




    }
}
