using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class CategoryDAL
    {
        private ASMProject1Entities db;
        public CategoryDAL()
        {
            db = new ASMProject1Entities();
        }
        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }
        public List<ProductAndOrderInfo> GetCateGory()
        {
            var products = db.Products.ToList();
            var categoriesWithProducts = products.Select(p => new ProductAndOrderInfo
            {
                Product = p,
            }).ToList();
            return categoriesWithProducts;
        }

        public List<ProductAndOrderInfo> GetProductByCateAndPersonId(int categoryId, int personId)
        {
            var products = categoryId == -1
              ? db.Products.ToList()
              : db.Products.Where(p => p.CategoryID == categoryId).ToList();

            var orderInfo = (from oi in db.OrderInfoes
                             join o in db.Orders on oi.OrderID equals o.OrderID
                             where o.PersonID == personId
                             select new
                             {
                                 oi.OrderID,
                                 oi.ProductID,
                                 oi.Quantity,
                                 o.Status
                             }).ToList();


            var productsWithOrderInfo = products.Select(p =>
            {
                var orderInfoItem = orderInfo.FirstOrDefault(oi => oi.ProductID == p.ProductID);
                var quantity = (byte)(orderInfoItem?.Quantity ?? 0);

                if (orderInfoItem != null && categoryId != -1 && orderInfoItem.Status == true)
                {
                    quantity = 0;
                }

                return new ProductAndOrderInfo
                {
                    Product = p,
                    orderInfo = new OrderInfo { Quantity = quantity }
                };
            }).ToList();

            return productsWithOrderInfo;


        }





        public List<ProductAndOrderInfo> GetProductsWithOrderInfo(int personID)
        {
            // Lấy tất cả sản phẩm
            var products = db.Products.ToList();

            // Lấy bản ghi từ bảng OrderInfo theo PersonID
            var orderInfo = (from oi in db.OrderInfoes
                             join o in db.Orders on oi.OrderID equals o.OrderID
                             where o.PersonID == personID
                             select new
                             {
                                 oi.ProductID,
                                 oi.Quantity
                             }).ToList();

            // Tạo một Dictionary để lưu thông tin OrderInfo theo ProductID
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


 

        public List<Product> GetAllProduct()
        {
            List<Product> products = db.Products.ToList();
            return products;
        }
    }
}
