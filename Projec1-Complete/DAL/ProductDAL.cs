using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class ProductDAL
    {
        private ASMProject1Entities db;
        public ProductDAL()
        {
            db = new ASMProject1Entities();
        }

        public int AddHistoryRecord()
        {
            int maxHistoryID = db.Histories.Max(h => (int?)h.HistoryID) ?? 0;
            int newHistoryID = maxHistoryID + 1;
            return newHistoryID;
        }

        public List<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        public Product GetProduct(string productID)
        {
            var prd = db.Products.FirstOrDefault(p => p.ProductID == productID);
            return prd;
        }
        public void AddProduct(Product product)
        {
            db.Products.Add(product);

            db.SaveChanges();
            var historyRecord = new History
            {
                HistoryID = AddHistoryRecord(),
                Action = "Thêm Sản Phẩm",
                TimeStamp = DateTime.Now,
                AccountID = 1,
                ProductID = product.ProductID,
            };
            db.Histories.Add(historyRecord);
            db.SaveChanges();

        }
        public bool ProductIDExists(string productID)
        {
            return db.Products.Any(p => p.ProductID == productID);
        }
        public void UpdateProduct(Product product)

        {
            var prd = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (prd != null)
            {
                prd.ProductID = product.ProductID;
                prd.ProductName = product.ProductName;
                prd.Quantity = product.Quantity;
                prd.Price = product.Price;
                prd.PriceSell = product.PriceSell;
                prd.Description = product.Description;
                prd.ImageLink = product.ImageLink;
                var historyRecord = new History
                {
                    HistoryID = AddHistoryRecord(),
                    Action = "Sửa Sản Phẩm",
                    TimeStamp = DateTime.Now,
                    AccountID = 1,
                    ProductID = product.ProductID,
                };
                db.Histories.Add(historyRecord);
                db.SaveChanges();


            }
            db.Products.ToList();


        }
        public void DeleteProduct(string id)
        {
            var prd = db.Products.FirstOrDefault(p => p.ProductID == id);
            if (prd != null)
            {
                db.Products.Remove(prd);

                db.SaveChanges();
                var historyRecord = new History
                {
                    HistoryID = AddHistoryRecord(),
                    Action = "Xóa Sản Phẩm",
                    TimeStamp = DateTime.Now,
                    AccountID = 1,
                    ProductID = id,
                };
                db.Histories.Add(historyRecord);
                db.SaveChanges();

                db.Products.ToList();


            }

        }

        public string GetLatestProductID()
        {
            var prd = db.Products.OrderByDescending(p => p.ProductID).FirstOrDefault();
            return prd?.ProductID;
        }



        public List<Product> SearchPrd(string search)
        {

            var list = db.Products.Where(p => p.ProductID.ToString().Contains(search) || p.ProductName.Contains(search) || p.Status.Contains(search)).ToList();

            return list;
        }

    }
}
