using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class ProductAndOrderInfo
    {
        public Product Product { get; set; }
        public Order order { get; set; }
        public Person person { get; set; }
        public Category category { get; set; }
        public OrderInfo orderInfo { get; set; }
        public string DisplayStatus { get; set; }
        public decimal TotalAmount  { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsSelected { get; set; }

    }
}
