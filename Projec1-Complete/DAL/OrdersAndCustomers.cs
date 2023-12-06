using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class OrdersAndCustomers
    {
        public Order order { get; set; }
        public OrderInfo orderInfo { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public string ProductName { get; set; }
        public decimal PriceSell { get; set; }
        public byte Discount { get; set; }

    }
}
