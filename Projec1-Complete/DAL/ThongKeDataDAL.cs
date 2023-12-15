using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class ThongKeDataDAL
    {
        private ASMProject1Entities db;
        public ThongKeDataDAL()
        {
            db = new ASMProject1Entities();
        }
       
        public List<ThongKeData> GetRevenueBy(string groupBy)
        {
            var revenueData = new List<ThongKeData>();

            switch (groupBy)
            {
                case "Date":
                    revenueData = db.Orders
                        .Join(db.OrderInfoes, o => o.OrderID, oi => oi.OrderID, (o, oi) => new { Order = o, OrderInfo = oi })
                        .Join(db.Products, joined => joined.OrderInfo.ProductID, p => p.ProductID, (joined, p) => new { joined.Order, joined.OrderInfo, Product = p })
                        .Where(joined => joined.Order.Status == true)
                        .GroupBy(joined => DbFunctions.TruncateTime(joined.Order.OrderDate))
                        .Select(g => new ThongKeData
                        {
                            Date = (DateTime)g.Key,
                            TotalRevenue = (decimal)g.Sum(joined => joined.Product.PriceSell * joined.OrderInfo.Quantity - (1 - joined.Order.Discount))

                        })
                        .OrderBy(td => td.Date)
                        .ToList();
                    break;
                case "Month":
                    revenueData = db.Orders
                        .Join(db.OrderInfoes, o => o.OrderID, oi => oi.OrderID, (o, oi) => new { Order = o, OrderInfo = oi })
                        .Join(db.Products, joined => joined.OrderInfo.ProductID, p => p.ProductID, (joined, p) => new { joined.Order, joined.OrderInfo, Product = p })
                        .Where(joined => joined.Order.Status == true)
                        .GroupBy(joined => new { Year = joined.Order.OrderDate.Value.Year, Month = joined.Order.OrderDate.Value.Month })
                        .Select(g => new
                        {
                            Year = g.Key.Year,
                            Month = g.Key.Month,
                            TotalRevenue = (decimal)g.Sum(joined => joined.Product.PriceSell * joined.OrderInfo.Quantity - (1 - joined.Order.Discount))
                        })
                        .AsEnumerable()
                        .Select(g => new ThongKeData
                        {
                            Date = new DateTime(g.Year, g.Month, 1),

                            TotalRevenue = g.TotalRevenue
                        })
                        .OrderBy(td => td.Date)
                        .ToList();
                    break;


                case "Product":
                    revenueData = (from oi in db.OrderInfoes
                                   join p in db.Products on oi.ProductID equals p.ProductID
                                   where oi.Order.Status == true
                                   group new { oi, p } by new { p.ProductID, p.ProductName } into g
                                   orderby g.Key.ProductID
                                   select new ThongKeData
                                   {
                                       ProductId = g.Key.ProductID,
                                       ProductName = g.Key.ProductName,
                                       TotalRevenue = (decimal)g.Sum(x => x.oi.Quantity * x.p.PriceSell)
                                   }).ToList();
                    break;


                default:
                    throw new ArgumentException($"Invalid groupBy value: {groupBy}");
            }

            return revenueData;
        }

    }
}
