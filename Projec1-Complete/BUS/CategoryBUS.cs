using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class CategoryBUS
    {
        private CategoryDAL _categoryDAL;
        public CategoryBUS()
        {
            _categoryDAL = new CategoryDAL();
        }
        public List<Category> GetCategories()
        {
            return _categoryDAL.GetCategories();
        }
        public List<Product> GetProductByID(int id)
        {
            return _categoryDAL.GetProductByID(id);
        }
    }
}
