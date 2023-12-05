using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class ProductBUS
    {
        public ProductDAL productDAL { get; set; }
        public ProductBUS()
        {
            productDAL = new ProductDAL();
        }
        public List<Product> GetProducts()
        { return productDAL.GetProducts(); }
        public void AddProduct(Product product)
        { productDAL.AddProduct(product); }
        public void UpdateProduct(Product prd)
        {
            productDAL.UpdateProduct(prd);
        }
        public Product GetProduct(string id)
        {
            return productDAL.GetProduct(id);
        }
        public string GetLastestProductID()
        {
            return productDAL.GetLatestProductID();
        }
        public bool ProductIDExists(string productID)
        {
            return productDAL.ProductIDExists(productID);
        }
        public void DelProduct(string id)
        {
            productDAL.DeleteProduct(id);
        }
        public List<Product> SearchPrd(string search)
        {
            return productDAL.SearchPrd(search);
        }

    }
}
